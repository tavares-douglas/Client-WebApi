using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechnicalCase_PBTech.Controllers;
using TechnicalCase_PBTech.Data;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace TechnicalCase_PBTech.Tests.Controllers
{
    [TestClass]
    public class ClientControllerTest
    {
        [TestMethod]
        public void ClientController_GetClients_ReturnsOk()
        {
            List<Client> clients = GetTestClients();
            DataContext dataContext = CreateContext();
            dataContext.Clients.AddRange(clients);
            dataContext.SaveChanges();

            ClientController clientController = new ClientController(dataContext);

            var queryResult = clientController.GetClients();
            Assert.AreEqual(clients, queryResult.Result.Value);
        }

        [TestMethod]
        public void ClientController_GetClient_ReturnsOk()
        {
            List<Client> clients = GetTestClients();
            DataContext dataContext = CreateContext();

            dataContext.Clients.AddRange(clients);
            ClientController clientController = new ClientController(dataContext);

            var queryResult = clientController.GetClient(clients[0].Email);
            Assert.AreEqual(clients[0], queryResult.Result.Value);
        }

        [TestMethod]
        public void ClientController_GetClient_ReturnsBadRequest()
        {
            List<Client> clients = GetTestClients();
            DataContext dataContext = CreateContext();
            dataContext.Clients.AddRange(clients);
            ClientController clientController = new ClientController(dataContext);

            String nonExistingClient = "brettmaher@cowboys.com";
            var queryResult = clientController.GetClient(nonExistingClient);
            Assert.AreEqual("Client not found.", queryResult.Result.Value);
        }

        [TestMethod]
        public void ClientController_AddClient_ReturnsOk()
        {
            List<Client> clients = GetTestClients();
            DataContext dataContext = CreateContext();
            dataContext.Clients.AddRange(clients);
            ClientController clientController = new ClientController(dataContext);

            Client clientToBeAdded = new Client { Email = "larryfitzgerald@cardinals.com", Name = "Larry Fitzgerald" };
            var queryResult = clientController.AddClient(clientToBeAdded);
            clients.Add(clientToBeAdded);
            Assert.AreEqual(clients, queryResult.Result.Value);
        }

        [TestMethod]
        public void ClientController_AddClient_ReturnsEmailNotAllowedBecauseOfAtSymbol()
        {
            List<Client> clients = GetTestClients();
            DataContext dataContext = CreateContext();
            dataContext.Clients.AddRange(clients);
            ClientController clientController = new ClientController(dataContext);

            Client clientToBeAdded = new Client { Email = "larryfitzgerald", Name = "Larry Fitzgerald" };
            var queryResult = clientController.AddClient(clientToBeAdded);
            Assert.AreEqual("E-mail not allowed.", queryResult.Result.Value);
        }

        [TestMethod]
        public void ClientController_AddClient_ReturnsEmailNotAllowedBecauseOfDotSymbol()
        {
            List<Client> clients = GetTestClients();
            DataContext dataContext = CreateContext();
            dataContext.Clients.AddRange(clients);
            ClientController clientController = new ClientController(dataContext);

            Client clientToBeAdded = new Client { Email = "larryfitzgerald@outlook", Name = "Larry Fitzgerald" };
            var queryResult = clientController.AddClient(clientToBeAdded);
            Assert.AreEqual("E-mail not allowed.", queryResult.Result.Value);
        }

        //[TestMethod]
        //public void ClientController_UpdateClient_ReturnsOk()
        //{
        //    var clients = GetTestClients();
        //    var dataContext = A.Fake<DataContext>();
        //    dataContext.Add(clients);
        //    ClientController clientController = new ClientController(dataContext);

        //    String clientToBeUpdatedEmail = "larryfitzgerald@outlook.com";
        //    Client clientToBeUpdated = new Client { Email = "larryfitzpatrick@outlook.com", Name = "Larry Fitzpatrick" };
        //    var queryResult = clientController.UpdateClient(clientToBeUpdatedEmail, clientToBeUpdated);
        //    Assert.AreEqual(queryResult, "E-mail not allowed.");
        //}

        [TestMethod]
        public void ClientController_DeleteClient_ReturnsOk()
        {
            List<Client> clients = GetTestClients();
            DataContext dataContext = CreateContext();
            dataContext.Clients.AddRange(clients);
            ClientController clientController = new ClientController(dataContext);

            var queryResult = clientController.DeleteClient(clients[2].Email);
            clients.RemoveAt(2);
            Assert.AreEqual(clients, queryResult.Result.Value);
        }

        [TestMethod]
        public void ClientController_DeleteClient_ReturnsBadRequest()
        {
            List<Client> clients = GetTestClients();
            DataContext dataContext = CreateContext();
            dataContext.Clients.AddRange(clients);
            ClientController clientController = new ClientController(dataContext);

            String nonExistingClientToBeDeleted = "brettmaher@cowboys.com";
            var queryResult = clientController.DeleteClient(nonExistingClientToBeDeleted);
            Assert.AreEqual("Client not found.", queryResult.Result.Value);
        }

        private List<Client> GetTestClients()
        {
            List<Client> testClients = new List<Client>
            {
                new Client { Email = "jalenhurts@eagles.com", Name = "Jalen Hurts" },
                new Client { Email = "tuatagovailoa@dolphins.com", Name = "Tua Tagovailoa" },
                new Client { Email = "jjwatt@cardinals", Name = "JJ Watt" }
            };

            return testClients;
        }

        private DataContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
            var databaseContext = new DataContext(options);
            databaseContext.Database.EnsureCreated();

            return databaseContext;
        }
    }
}
