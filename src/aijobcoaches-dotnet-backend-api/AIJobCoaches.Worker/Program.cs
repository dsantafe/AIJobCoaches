using AIJobCoaches.Application.Interfaces;
using AIJobCoaches.Application.Services;
using AIJobCoaches.Domain.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

class Program
{
    static async Task Main(string[] args)
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
        builder.Configuration.AddJsonFile("appsettings.json");

        builder.Services.AddScoped<ITrainingService, TrainingService>();
        builder.Services.AddScoped<ICourseService, CourseService>();
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddDbContext<AIJobCoachesContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("AI_JOB_COACHES_CONNECTION_STRING")));
        using IHost host = builder.Build();

        await ScrapeCourses(host.Services);
        await host.RunAsync();
    }

    static async Task ScrapeCourses(IServiceProvider hostProvider)
    {
        using IServiceScope serviceScope = hostProvider.CreateScope();
        IServiceProvider provider = serviceScope.ServiceProvider;
        ICourseService courseService = provider.GetRequiredService<ICourseService>();
        await courseService.CreateCourses();
    }    
}