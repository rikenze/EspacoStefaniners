using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace EspacoStefaniners.IntegrationTests.Tests
{
    public class IntegrationBarService
    {
         [Fact]
        public async Task GetWebResourceRootReturnsOkStatusCode()
        {
            // Arrange
            var appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.EspacoStefaniners_AppHost>();
            appHost.Services.ConfigureHttpClientDefaults(clientBuilder =>
            {
                clientBuilder.AddStandardResilienceHandler();
            });
            await using var app = await appHost.BuildAsync();
            var resourceNotificationService = app.Services.GetRequiredService<ResourceNotificationService>();

            await app.StartAsync();

            // Act
            var httpClient = app.CreateHttpClient("barservice");
            await resourceNotificationService.WaitForResourceAsync("barservice", KnownResourceStates.Running).WaitAsync(TimeSpan.FromSeconds(30));

            var response = await httpClient.GetAsync("/bebidas");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }}