using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalCase_PBTech.Data;
using TechnicalCase_PBTech.Models;
using TechnicalCase_PBTech.Repository;
using Xunit;

namespace TechnicalCase_PBTechTests.Tests.Repository
{
    public class ClientRepositoryTests
    {
        private async Task<DataContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new DataContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Clients.CountAsync() <= 0)
            {
                databaseContext.Clients.Add(
                new Client()
                    {
                        Name = "Baker Mayfield",
                        Email = "bakermayfield@osu.com"
                });
                await databaseContext.SaveChangesAsync();
            }
            return databaseContext;
        }
        [Fact]
        public async void ClientRepository_GetClient_ReturnsClient()
        {
            //Arrange
            var Email = "bakermayfield@osu.com";
            var dbContext = await GetDatabaseContext();
            var clientRepository = new ClientRepository(dbContext);

            //Act
            var result = clientRepository.GetClient(Email);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Client>();
        }
    }
}
