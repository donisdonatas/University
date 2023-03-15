using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using UniExam.BusinessLogic.Interfaces;
using UniExam.BusinessLogic.Models;
using UniExam.BusinessLogic.Services;
using UniExam.Repository.Data;
using UniExam.Repository.Interfaces;
using UniExam.Repository.Repository;

namespace Uni.Presentation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            IHost host = CreateHostBuilder(args).Build();
            var departmentRepositoryService = host.Services.GetRequiredService<IDepartmentRepositoryService>();
            var lectureRepositoryService = host.Services.GetRequiredService<ILectureRepositoryService>();
            var studentRepositoryService = host.Services.GetRequiredService<IStudentRepositoryService>();

            var defaultDatabaseService = host.Services.GetRequiredService<DefaultDatabaseService>();
            defaultDatabaseService.CheckDefaultTables();

            Menu menu = new Menu(departmentRepositoryService, lectureRepositoryService, studentRepositoryService);
            menu.GetMainMenu();

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<UniContext>(options => { options.UseSqlServer(hostContext.Configuration.GetConnectionString("UniConnection")); }, ServiceLifetime.Scoped);
                    services.AddScoped<IDepartmentRepository, DepartmentRepository>();
                    services.AddScoped<ILectureRepository, LectureRepository>();
                    services.AddScoped<IStudentRepository, StudentRepository>();
                    services.AddScoped<IDepartmentRepositoryService, DepartmentRepositoryService>();
                    services.AddScoped<ILectureRepositoryService, LectureRepositoryService>();
                    services.AddScoped<IStudentRepositoryService, StudentRepositoryService>();
                    services.AddScoped<DefaultDatabaseService>();
                });
        }
    }
}