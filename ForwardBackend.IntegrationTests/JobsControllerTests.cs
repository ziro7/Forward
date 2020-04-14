using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using ForwardBackend.Models;
using Xunit;

namespace ForwardBackend.IntegrationTests
{
    public class JobsControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetJobs_WithoutBearerToken_ReturnsError() {
            
            // Arrange

            // Act
            var response = await _httpClient.GetAsync($"api/jobs");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task GetJobs_WithBearerToken_ReturnsAllJobs() {

            // Arrange
            await Authenticate(); 

            // Act
            var response = await _httpClient.GetAsync($"api/jobs");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsStreamAsync()).Should().NotBeNull();
        }

        [Fact]
        public async Task GetJob_UnknownId_ReturnNotFound() {

            // Arrange
            await Authenticate();

            // Act
            var response = await _httpClient.GetAsync($"api/jobs/{9999}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetJob_KnownId_ReturnOk() {

            // Arrange
            await Authenticate();

            // Act
            var response = await _httpClient.GetAsync($"api/jobs/{11}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetJob_KnownJob_ReturnJob() {

            // Arrange
            await Authenticate();

            // Act
            var result = await JsonSerializer.DeserializeAsync<Job>(await _httpClient.GetStreamAsync($"api/jobs/{11}"), new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
            });

            // Assert
            result.Should().BeOfType<Job>();
        }
    }
}
