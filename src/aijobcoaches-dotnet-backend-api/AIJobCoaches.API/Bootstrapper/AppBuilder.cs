namespace AIJobCoaches.API.Bootstrapper
{
    using AIJobCoaches.Application.Interfaces;
    using AIJobCoaches.Application.Services;
    using AIJobCoaches.Domain.Constants;
    using AIJobCoaches.Domain.Data;
    using FluentValidation;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.OpenApi.Models;
    using System.Reflection;

    /// <summary>
    /// App Builder 
    /// </summary>
    public static class AppBuilder
    {
        /// <summary>
        /// Get App
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static WebApplication GetApp(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddHealthChecks();
            MemoryCache memoryCache = new(new MemoryCacheOptions { SizeLimit = CacheConstant.SizeLimit });
            builder.Services.AddSingleton<IMemoryCache>(memoryCache);
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), ServiceLifetime.Scoped);
            builder.Services.AddDbContext<AIJobCoachesContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AI_JOB_COACHES_CONNECTION_STRING")));

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "AI for supported employment job coaches",
                    Description = "AI for supported employment job coaches challenge.",
                    Contact = new OpenApiContact
                    {
                        Name = "Microsoft Learn",
                        Url = new Uri("https://learn.microsoft.com/")
                    }
                });

                // Incluir el archivo XML de comentarios si está habilitado
                string xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy => policy.AllowAnyOrigin()
                                    .AllowAnyMethod()
                                    .AllowAnyHeader());
            });

            // Services
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<ITrainingService, TrainingService>();
            builder.Services.AddScoped<ITopicService, TopicService>();
            builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
            builder.Services.AddScoped<ICourseService, CourseService>();
            builder.Services.AddScoped<IQuizResultService, QuizResultService>();

            // Logs
            builder.Logging.ClearProviders();

            WebApplication app = builder.Build();
            return app;
        }
    }
}
