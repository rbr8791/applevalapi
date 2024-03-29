using System.Collections.Generic;
using dwCheckApt.Tests.Helpers;
using Moq;
using Xunit;
using System.IO;
using System;
using applevalApi.Entities;
using applevalApi.Persistence;
using applevalApi.Persistence.Helpers;

namespace applevalApi.Tests
{
    public class DatabaseSeederTests
    {
        [Fact]
        public async void DbSeeder_SeedBookData_NoDataSupplied_ShouldThrowException()
        {
            // Arrange
            var bookList = new List<Book>();
            var mockBookSet = DbSetHelpers.GetQueryableDbSet(bookList);
            var mockset = new Mock<ApplDbContext>();
            mockset.Setup(m => m.Books).Returns(mockBookSet.Object);

            // Act & Assert
            var dbSeeder = new DatabaseSeeder(mockset.Object);
            var argEx = await Assert.ThrowsAsync<ArgumentException>(() =>
                dbSeeder.SeedBookEntitiesFromJson(string.Empty));
        }

        [Fact]
        public void DbSeeder_SeedBookData_DataSupplied_ShouldNotThrowException()
        {
            // Arrange
            // TODO Add an interface here, to mock stuff properly
            var bookList = new List<Book>();
            var mockBookSet = DbSetHelpers.GetQueryableDbSet(bookList);
            var mockset = new Mock<ApplDbContext>();
            mockset.Setup(m => m.Books).Returns(mockBookSet.Object);
            var testJsonDirectory = Path.Combine(Directory.GetCurrentDirectory(), "SeedData");
            var pathToSeedData = Path.Combine(testJsonDirectory, "TestBookSeedData.json");
            
            // Act & Assert
            var dbSeeder = new DatabaseSeeder(mockset.Object);
            
            dbSeeder.SeedBookEntitiesFromJson(pathToSeedData).Wait();
        }
    }
}