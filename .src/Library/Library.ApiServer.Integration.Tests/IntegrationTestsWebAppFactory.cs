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
using Hangfire;
using System.Runtime.InteropServices;
using Hangfire.PostgreSql;

namespace Library.ApiServer.IntegrationTests
{
    public class IntegrationTestsWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
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
                // unregister previously rgistered services
                var descriptor = services.SingleOrDefault(e => e.ServiceType == typeof(DbContextOptions<LibraryContext>));
                if (descriptor is not null)
                {
                    services.Remove(descriptor);
                }
                services.AddDbContext<LibraryContext>(options =>
                {
                    options.UseNpgsql(_libraryContainer.GetConnectionString());
                });

                var hangfireDescriptors = services.Where(e => e.ServiceType.ToString().ToLower().Contains("hangfire")).ToList();
                foreach (var hangfireDescriptor in hangfireDescriptors)
                {
                    services.Remove(hangfireDescriptor);
                }
                services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(e => e.UseNpgsqlConnection(_hangfireContainer.GetConnectionString())));
                services.AddHangfireServer();
            });
        }

        async Task IAsyncLifetime.DisposeAsync()
        {
            await _libraryContainer.StopAsync();
            await _hangfireContainer.StopAsync();
        }

        public async Task InitializeAsync()
        {
            await _libraryContainer.StartAsync();
            await _hangfireContainer.StartAsync();
        }
    }
}