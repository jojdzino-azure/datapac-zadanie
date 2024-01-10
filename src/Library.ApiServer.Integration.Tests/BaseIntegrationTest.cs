using Library.ApiServer.IntegrationTests;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistance;

namespace Library.ApiServer.Integration.Tests
{
    public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestsWebAppFactory>, IAsyncLifetime
    {
        protected readonly IServiceScope _scope;
        protected readonly IMediator mediator;

        public BaseIntegrationTest(IntegrationTestsWebAppFactory factory)
        {
            _scope = factory.Services.CreateScope();
            mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        public async Task InitializeAsync()
        {
            var context = _scope.ServiceProvider.GetRequiredService<LibraryContext>();
            await context.Database.MigrateAsync();
        }
    }
}