using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Core.Tests
{
    public class JobTests
    {
        private IList<ValidationResult> ValidateModel(object model) {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults;
        }

        [Fact]
        public void CompanyNameRequired_CompanyNameIsSupplied_Valid() {
            var job = new Job() { CompanyName = "TestCompany" };
            Assert.True(ValidateModel(job).Contains(new ValidationResult("Company name is too long")));
        }


        [Fact]
        public void IsValid_ValidInout_ReturnTrue() {
            // Arrange
            var job = new Job() {
                CompanyName = "Company1",
                StartDate = new DateTime(2018, 12, 1),
                EndDate = new DateTime(2020, 12, 1),
                WorkExperiences = new List<WorkExperience>() { new WorkExperience() {
                    Titel="Developer",
                    FromDate=new DateTime(2018,12,1),
                    EndDate=new DateTime(2020,12,1)
                } }
            };
            // Act
            var result = job.IsValid();
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValid_InvalidStartDates_ReturnFalse() {
            // Arrange
            var job = new Job() {
                CompanyName = "Company1",
                StartDate = new DateTime(2018, 12, 1),
                EndDate = new DateTime(2020, 12, 1),
                WorkExperiences = new List<WorkExperience>() { new WorkExperience() { 
                    Titel="Developer",
                    FromDate=new DateTime(2001,12,1),
                    EndDate=new DateTime(2020,12,1)
                } }
            };
            // Act
            var result = job.IsValid();
            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValid_InvalidEndDates_ReturnFalse() {
            // Arrange
            var job = new Job() {
                CompanyName = "Company1",
                StartDate = new DateTime(2018, 12, 1),
                EndDate = new DateTime(2020, 12, 1),
                WorkExperiences = new List<WorkExperience>() { new WorkExperience() {
                    Titel="Developer",
                    FromDate=new DateTime(2018,12,1),
                    EndDate=new DateTime(2021,12,1)
                } }
            };
            // Act
            var result = job.IsValid();
            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValid_NewJob_ReturnTrue() {
            // Arrange
            var job = new Job() {
                CompanyName = "Company1",
                StartDate = new DateTime(2018, 12, 1),
                EndDate = new DateTime(2020, 12, 1),
                WorkExperiences = new List<WorkExperience>()
        };
            // Act
            var result = job.IsValid();
            // Assert
            Assert.True(result);
        }
    }
}
