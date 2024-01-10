using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.PostgreSql;
using WebApplication1;

namespace Library.ApiServer.IntegrationTests
{
    public class IntegrationTestsWebAppFactory : WebApplicationFactory<Program>
    {
        private readonly PostgreSqlContainer _libraryContainer = new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .WithDatabase("Library")
            .WithUsername("postgres")
            .WithPassword("password")
            .Build();

        private readonly PostgreSqlContainer _hangfireContainer = new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .WithDatabase("libraryHangfire")
            .WithUsername("postgres")
            .WithPassword("password")
            .Build();
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                var descriptor = services.SingleOrDefault(e => e.ServiceType == typeof(DbContextOptions<LibraryContext>));
                if (descriptor is not null)
                {
                    services.Remove(descriptor);
                }
                services.AddDbContext<LibraryContext>(options =>
                {
                    options.UseNpgsql()
                });
            });
        }
    }
}