using Common.MediatR;
using Contracts.Validation.Book;
using Domain.Repositories.Book;
using Domain.Repositories.Borrowing;
using Domain.Repositories.User;
using FluentValidation;
using Hangfire;
using Hangfire.PostgreSql;
using Library.ApiServer;
using Library.ApiServer.Emails;
using Library.ApiServer.Emails.Smtp;
using MediatR;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using Persistance;
using Persistance.Repositories;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(e=>
            e.JsonSerializerOptions.DefaultIgnoreCondition= System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            );
            builder.Services.AddSwaggerGen();
            var connectionString = builder.Configuration.GetConnectionString("LibraryContext");
            builder.Services.AddDbContext<LibraryContext>(
                options => options
                        .UseNpgsql(connectionString)
                        .LogTo(Console.WriteLine, LogLevel.Information)
                        );
            builder.Services.AddTransient<ExceptionHandlingMiddleware>();
            
            ConfigureOptions(builder);
            ConfigureMediatR(builder);
            ConfigureRespositories(builder);
            ConfigureHangfire(builder);
            ConfigureMailing(builder);
            builder.Services.AddValidatorsFromAssemblyContaining(typeof(CreateBookCommandValidator));
            var app = builder.Build();
            //migrate DB
            using var scope = app.Services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<LibraryContext>();
            context.Database.Migrate();
            
            Console.WriteLine("Done");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            ScheduleJobs(app);
            // Configure the HTTP request pipeline.
            app.UseRouting();
            //app.UseAuthorization();

            app.MapControllers();

            app.Run();


        }

        private static void ConfigureMailing(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ISmtpClient, EmptySmtpClient>();
        }

        private static void ConfigureOptions(WebApplicationBuilder builder)
        {
            builder.Services.Configure<SmtpSettings>(
                builder.Configuration.GetSection(nameof(SmtpSettings)));
        }

        private static void ScheduleJobs(WebApplication app)
        {
            var manager = app.Services.CreateScope().ServiceProvider.GetService<IRecurringJobManager>();
            var hourly = Cron.Hourly();
            manager.AddOrUpdate<EmailSender>("EmailSender", e => e.SendMails(), hourly); ;//every hour
        }

        private static void ConfigureRespositories(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IQueryBookRepository, BooksRepository>();
            builder.Services.AddScoped<IBookRepository, BooksRepository>();
            builder.Services.AddScoped<IQueryUserRepository, UserRepository>();
            builder.Services.AddScoped<IBorrowingRepository, BorrowingRepository>();
        }

        private static void ConfigureMediatR(WebApplicationBuilder builder)
        {
            builder.Services.AddMediatR(e =>
            {
                e.RegisterServicesFromAssembly(typeof(Handlers.AssemblyReference).Assembly);
            });
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }

        private static void ConfigureHangfire(WebApplicationBuilder builder)
        {
            var hangfireConnection = builder.Configuration.GetConnectionString("HangfireConnection");
            builder.Services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(e => e.UseNpgsqlConnection(hangfireConnection)));
            builder.Services.AddHangfireServer();
        }
    }
}