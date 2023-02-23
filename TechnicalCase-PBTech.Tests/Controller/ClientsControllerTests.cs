using FakeItEasy;
using Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalCase_PBTech.Controllers;
using TechnicalCase_PBTech.Data;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace TechnicalCase_PBTech.Tests.Controller
{
    [TestClass]
    public class ClientControllerTest
    {
        [TestMethod]
        public void ClientController_GetClients_ReturnsOk()
        {
            var clients = GetTestClients();
            var dataContext = A.Fake<DataContext>();
            dataContext.Add(clients);
            ClientController clientController = new ClientController(dataContext);

            var queryResult = clientController.GetClients() as IEnumerable<Client>;
            Assert.AreEqual(queryResult, clients);
        }

        [TestMethod]
        public void ClientController_GetClient_ReturnsOk()
        {
            var clients = GetTestClients();
            var dataContext = A.Fake<DataContext>();
            dataContext.Add(clients);
            ClientController clientController = new ClientController(dataContext);

            var queryResult = clientController.GetClient("jalenhurts@eagles.com");
            Assert.Equals(queryResult, clients[0]);
        }

        [TestMethod]
        public void ClientController_GetClient_ReturnsBadRequest()
        {
            var clients = GetTestClients();
            var dataContext = A.Fake<DataContext>();
            dataContext.Add(clients);
            ClientController clientController = new ClientController(dataContext);

            var queryResult = clientController.GetClient("brettmaher@cowboys.com");
            Assert.Equals(queryResult, "Client not found.");
        }

        [TestMethod]
        public void ClientController_AddClient_ReturnsOk()
        {
            var clients = GetTestClients();
            var dataContext = A.Fake<DataContext>();
            dataContext.Add(clients);
            ClientController clientController = new ClientController(dataContext);

            Client clientToBeAdded = new Client { Email = "larryfitzgerald@cardinals.com", Name = "Larry Fitzgerald" });
            var queryResult = clientController.AddClient(clientToBeAdded);
            clients.Add(clientToBeAdded);
            Assert.Equals(queryResult, clients);
        }

        [TestMethod]
        public void ClientController_AddClient_ReturnsEmailNotAllowedBecauseOfAtSymbol()
        {
            var clients = GetTestClients();
            var dataContext = A.Fake<DataContext>();
            dataContext.Add(clients);
            ClientController clientController = new ClientController(dataContext);

            Client clientToBeAdded = new Client { Email = "larryfitzgerald", Name = "Larry Fitzgerald" });
            var queryResult = clientController.AddClient(clientToBeAdded);
            clients.Add(clientToBeAdded);
            Assert.Equals(queryResult, "E-mail not allowed.");
        }

        [TestMethod]
        public void ClientController_DeleteClient_ReturnsOk()
        {
            var clients = GetTestClients();
            var dataContext = A.Fake<DataContext>();
            dataContext.Add(clients);
            ClientController clientController = new ClientController(dataContext);

            var client = new Client();
            var queryResult = clientController.DeleteClient("jjwatt@cardinals");
            clients.RemoveAt(2);
            Assert.Equals(queryResult, clients);
        }

        [TestMethod]
        public void ClientController_DeleteClient_ReturnsBadRequest()
        {
            var clients = GetTestClients();
            var dataContext = A.Fake<DataContext>();
            dataContext.Add(clients);
            ClientController clientController = new ClientController(dataContext);

            var queryResult = clientController.DeleteClient("brettmaher@cowboys.com");
            Assert.Equals(queryResult, "Client not found.");
        }



        private List<Client> GetTestClients()
        {
            var testClients = new List<Client>();
            testClients.Add(new Client { Email = "jalenhurts@eagles.com", Name = "Jalen Hurts"});
            testClients.Add(new Client { Email = "tuatagovailoa@dolphins.com", Name = "Tua Tagovailoa" });
            testClients.Add(new Client { Email = "jjwatt@cardinals", Name = "JJ Watt" });

            return testClients;
        }
    }
}
