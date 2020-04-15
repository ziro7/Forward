using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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
            (await response.Content.ReadAsStreamAsync()).Should().NotBeNull(); //no jobs but still have content
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
            var newJob = new Job(11, "SimCorp", new DateTime(2018, 12, 01), new DateTime(2020, 12, 01));
            await AddJobToDatabase(newJob);

            // Act
            var response = await _httpClient.GetAsync($"api/jobs/{11}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // Cleanup
            var jobJson = new StringContent(JsonSerializer.Serialize(newJob), Encoding.UTF8, "application/json");
            await DeleteJobInDatabase(newJob.JobId, jobJson);
        }

        [Fact]
        public async Task GetJob_KnownJob_ReturnOkValidateJob() {

            // Arrange
            await Authenticate();
            var newJob = new Job(11, "SimCorp", new DateTime(2018, 12, 01), new DateTime(2020, 12, 01));
            await AddJobToDatabase(newJob);
            
            // Act
            var result = await JsonSerializer.DeserializeAsync<Job>(await _httpClient.GetStreamAsync($"api/jobs/{11}"), new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
            });

            // Assert
            result.Should().BeOfType<Job>();
            result.JobId.Should().Be(11);
            result.CompanyName.Should().Be("SimCorp");

            // Cleanup
            var jobJson = new StringContent(JsonSerializer.Serialize(newJob), Encoding.UTF8, "application/json");
            await DeleteJobInDatabase(newJob.JobId, jobJson);
        }

        [Fact]
        public async Task PostJob_AddingNewJob_ReturnOk() {

            // Arrange
            await Authenticate();
            var newJob = new Job(10, "Shantz", new DateTime(2015, 05, 01), new DateTime(2017, 01, 01));
            var jobJson = new StringContent(JsonSerializer.Serialize(newJob), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("api/jobs", jobJson);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            // Cleanup
            await DeleteJobInDatabase(newJob.JobId, jobJson);
        }

        [Fact]
        public async Task PostJob_AddingExistingJob_ReturnInternalServerError() {

            // Arrange
            await Authenticate();
            var newJob = new Job(11, "SimCorp", new DateTime(2018, 12, 01), new DateTime(2020, 12, 01));
            await AddJobToDatabase(newJob);
            var sameJob = new Job(11, "SimCorp", new DateTime(2018, 12, 01), new DateTime(2020, 12, 01));
            var jobJson = new StringContent(JsonSerializer.Serialize(sameJob), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("api/jobs", jobJson);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);

            // Cleanup
            var newjobJson = new StringContent(JsonSerializer.Serialize(newJob), Encoding.UTF8, "application/json");
            await DeleteJobInDatabase(newJob.JobId, newjobJson);
            await DeleteJobInDatabase(sameJob.JobId, jobJson);

        }

        [Fact]
        public async Task PostJob_AddingNullJob_UnsupportedMediaType() {

            // Arrange
            await Authenticate();

            // Act
            var response = await _httpClient.PostAsync("api/jobs", null);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnsupportedMediaType);
        }


        [Fact]
        public async Task PutJob_KnownJobIsChanged_NoContent() {

            // Arrange
            await Authenticate();
            var newJob = new Job(11, "SimCorp", new DateTime(2018, 12, 01), new DateTime(2020, 12, 01));
            await AddJobToDatabase(newJob);
            var job = new Job(11, "SimCorp A/S", new DateTime(2018, 12, 01), new DateTime(2020, 12, 01));
            job.WorkExperiences.Add(new WorkExperience() {
                Titel = "Developer",
                Description = "Arbejdet i teamet ”Hakuna Matata” som hver tredje måned tager en til flere features i et product increment planning event, hvor vi i den efterfølgende periode arbejder på disse features. Der vælges hovedsagligt features på transaktions området. Arbejdet kan indeholde opgaver i APL, C#, OCaml og ML (Meta Language) og kan være både front-end eller back-end eller en kombination (Personligt har jeg ikke arbejdet med OCaml eller ML).",
                FromDate = new DateTime(2018, 12, 01),
                EndDate = new DateTime(2020, 12, 01),
                JobForeignKey = job.JobId
            });
            var jobJson = new StringContent(JsonSerializer.Serialize(job), Encoding.UTF8, "application/json");
            int jobId = job.JobId;
            
            // Act
            var response = await _httpClient.PutAsync($"api/jobs/{jobId}", jobJson);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

            // Cleanup
            await DeleteJobInDatabase(newJob.JobId, jobJson);
        }

        [Fact]
        public async Task DeleteJob_KnownJobIsDeleted_ReturnOk() {

            // Arrange
            await Authenticate();
            var knownJob = new Job(11, "SimCorp", new DateTime(2018, 12, 01), new DateTime(2020, 12, 01));
            await AddJobToDatabase(knownJob);

            // Act
            var response = await _httpClient.DeleteAsync($"api/jobs/{knownJob.JobId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
