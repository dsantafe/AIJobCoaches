namespace AIJobCoaches.Application.Services
{
    using AIJobCoaches.Application.DTOs;
    using AIJobCoaches.Application.Interfaces;
    using AIJobCoaches.Domain.Entities;
    using AIJobCoaches.Infrastructure.Managers;
    using AutoMapper;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using System.Threading.Tasks;

    public class CourseService(IMapper mapper,
        ITrainingService trainingService) : ICourseService
    {
        private readonly string cosmosCnx = ConfigurationManager.GetValue("COSMOS_CONNECTION_STRING");
        private readonly string cosmosDatabase = ConfigurationManager.GetValue("COSMOS_DATABASE");

        public IEnumerable<CourseDTO> GetCoursesByTraining(int trainingID)
        {
            CosmosDBManager<Course> cosmosDB = new(cosmosDatabase, "Courses", cosmosCnx);
            IEnumerable<Course> courses = cosmosDB.GetItemsAsync($"SELECT * FROM c WHERE c.TrainingID = {trainingID}")
                .GetAwaiter()
                .GetResult();
            return mapper.Map<IEnumerable<CourseDTO>>(courses);
        }

        public async Task CreateCourses()
        {
            string url = ConfigurationManager.GetValue("SCRAPING_URL");
            CosmosDBManager<Course> cosmosDB = new(cosmosDatabase, "Courses", cosmosCnx);
            IEnumerable<TrainingDTO> trainings = trainingService.GetTrainings();
            foreach (TrainingDTO training in trainings)
            {
                IEnumerable<Course> courses = await ScrapeCourses(training.TrainingID,
                    $"{url}?src=ukw&q={training.TrainingName}");
                foreach (var course in courses)
                    await cosmosDB.AddItemAsync(course);
            }
        }

        static async Task<IEnumerable<Course>> ScrapeCourses(int trainingID,
            string url)
        {
            List<Course> courses = [];
            ChromeOptions options = new();
            options.AddArgument("--disable-webgl");
            options.AddArgument("--headless=new");
            options.AddArgument("--enable-unsafe-swiftshader");
            options.AddArgument("--disable-gpu"); // Desactiva la aceleración por GPU
            options.AddArgument("--headless"); // Ejecuta en modo sin interfaz
            options.AddArgument("--no-sandbox"); // Evita problemas con permisos
            options.AddArgument("--disable-dev-shm-usage"); // Previene errores en contenedores
            options.AddArgument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64)");

            using IWebDriver driver = new ChromeDriver(options);
            try
            {
                driver.Navigate().GoToUrl(url);
                await Task.Delay(3000); // Esperar a que la página cargue

                var courseElements = driver
                    .FindElements(By.CssSelector(".course-card-module--container--3oS-F"))
                    .Take(5);

                foreach (var element in courseElements)
                {
                    try
                    {
                        var titleElement = element.FindElement(By.CssSelector(".course-card-title-module--course-title--wmFXN a"));
                        var linkElement = titleElement.GetAttribute("href");
                        var instructorElement = element.FindElement(By.CssSelector(".course-card-instructors-module--instructor-list--cJTfw"));
                        var ratingElement = element.FindElement(By.CssSelector(".ud-heading-sm.star-rating-module--rating-number--2-qA2"));

                        string title = titleElement.Text.Trim();
                        string courseUrl = linkElement;
                        string instructor = instructorElement.Text.Trim();
                        string rating = ratingElement.Text.Trim();

                        courses.Add(new Course
                        {
                            id = Guid.NewGuid().ToString(),
                            TrainingID = trainingID,
                            Title = title,
                            Url = courseUrl,
                            Instructor = instructor,
                            Rating = rating
                        });
                    }
                    catch (NoSuchElementException) { continue; }
                }
            }
            catch { }
            finally
            {
                driver.Quit();
            }

            return courses;
        }
    }
}
