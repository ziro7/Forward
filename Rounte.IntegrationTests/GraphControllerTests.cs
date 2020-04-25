using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Route.IntegrationTests
{
    public class GraphControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetPathFromBFS_ValidInput_ReturnsOK() {

            // Arrange
            int startPoint = 0;
            int endPoint = 5;

            // Act
            var response = await _httpClient.GetAsync($"api/graph/BFS/{startPoint}/{endPoint}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetPathFromBFS_ValidInput_ReturnsPath() {

            // Arrange
            int startPoint = 2;
            int endPoint = 1;

            // Act
            var result = await JsonSerializer.DeserializeAsync<List<int>>(await _httpClient.GetStreamAsync($"api/graph/BFS/{startPoint}/{endPoint}"));

            // Assert
            var expectedPath = new List<int> { 2, 3, 7, 5, 1 };
            result.Should().BeOfType<List<int>>();
            result.Should().BeEquivalentTo(expectedPath);
        }

        [Fact]
        public async Task GetPathFromDFS_ValidInput_ReturnsOK() {

            // Arrange
            int startPoint = 0;
            int endPoint = 5;

            // Act
            var response = await _httpClient.GetAsync($"api/graph/DFS/{startPoint}/{endPoint}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetPathFromDFS_ValidInput_ReturnsPath() {

            // Arrange
            int startPoint = 2;
            int endPoint = 1;

            // Act
            var result = await JsonSerializer.DeserializeAsync<List<int>>(await _httpClient.GetStreamAsync($"api/graph/DFS/{startPoint}/{endPoint}"));

            // Assert
            var expectedPath = new List<int> { 2, 3, 7, 5, 1 };
            result.Should().BeOfType<List<int>>();
            result.Should().BeEquivalentTo(expectedPath);
        }

        [Fact]
        public async Task GetPathFromDFS_6to3_ReturnsPath() {

            // Arrange
            int startPoint = 6;
            int endPoint = 3;

            // Act
            var result = await JsonSerializer.DeserializeAsync<List<int>>(await _httpClient.GetStreamAsync($"api/graph/DFS/{startPoint}/{endPoint}"));

            // Assert
            var expectedPath = new List<int> { 6, 5, 7, 4, 2, 3};
            result.Should().BeOfType<List<int>>();
            result.Should().BeEquivalentTo(expectedPath);
        }

        [Fact]
        public async Task GetPathFromBFS_6to3_ReturnsPath() {

            // Arrange
            int startPoint = 6;
            int endPoint = 3;

            // Act
            var result = await JsonSerializer.DeserializeAsync<List<int>>(await _httpClient.GetStreamAsync($"api/graph/BFS/{startPoint}/{endPoint}"));

            // Assert
            var expectedPath = new List<int> { 6, 5, 1, 2, 3 };
            result.Should().BeOfType<List<int>>();
            result.Should().BeEquivalentTo(expectedPath);
        }

        [Fact]
        public async Task GetPathFromBFS_InvalidInput_ReturnsError() {

            // Arrange
            int startPoint = 999;
            int endPoint = 3;

            // Act
            var response = await _httpClient.GetAsync($"api/graph/BFS/{startPoint}/{endPoint}");
            
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }
    }
}
