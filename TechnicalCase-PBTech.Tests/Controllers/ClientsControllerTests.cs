using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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

            var queryResult = clientController.GetClient(clients[0].Email);
            Assert.Equals(queryResult, clients[0]);
        }

        [TestMethod]
        public void ClientController_GetClient_ReturnsBadRequest()
        {
            var clients = GetTestClients();
            var dataContext = A.Fake<DataContext>();
            dataContext.Add(clients);
            ClientController clientController = new ClientController(dataContext);

            String nonExistingClientToBeDeleted = "brettmaher@cowboys.com";
            var queryResult = clientController.GetClient(nonExistingClientToBeDeleted);
            Assert.Equals(queryResult, "Client not found.");
        }

        [TestMethod]
        public void ClientController_AddClient_ReturnsOk()
        {
            var clients = GetTestClients();
            var dataContext = A.Fake<DataContext>();
            dataContext.Add(clients);
            ClientController clientController = new ClientController(dataContext);

            Client clientToBeAdded = new Client { Email = "larryfitzgerald@cardinals.com", Name = "Larry Fitzgerald" };
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

            Client clientToBeAdded = new Client { Email = "larryfitzgerald", Name = "Larry Fitzgerald" };
            var queryResult = clientController.AddClient(clientToBeAdded);
            Assert.Equals(queryResult, "E-mail not allowed.");
        }

        [TestMethod]
        public void ClientController_AddClient_ReturnsEmailNotAllowedBecauseOfDotSymbol()
        {
            var clients = GetTestClients();
            var dataContext = A.Fake<DataContext>();
            dataContext.Add(clients);
            ClientController clientController = new ClientController(dataContext);

            Client clientToBeAdded = new Client { Email = "larryfitzgerald@outlook", Name = "Larry Fitzgerald" };
            var queryResult = clientController.AddClient(clientToBeAdded);
            Assert.Equals(queryResult, "E-mail not allowed.");
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
        //    Assert.Equals(queryResult, "E-mail not allowed.");
        //}

        [TestMethod]
        public void ClientController_DeleteClient_ReturnsOk()
        {
            var clients = GetTestClients();
            var dataContext = A.Fake<DataContext>();
            dataContext.Add(clients);
            ClientController clientController = new ClientController(dataContext);

            var queryResult = clientController.DeleteClient(clients[2].Email);
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

            String nonExistingClientToBeDeleted = "brettmaher@cowboys.com";
            var queryResult = clientController.DeleteClient(nonExistingClientToBeDeleted);
            Assert.Equals(queryResult, "Client not found.");
        }



        private List<Client> GetTestClients()
        {
            var testClients = new List<Client>();
            testClients.Add(new Client { Email = "jalenhurts@eagles.com", Name = "Jalen Hurts" });
            testClients.Add(new Client { Email = "tuatagovailoa@dolphins.com", Name = "Tua Tagovailoa" });
            testClients.Add(new Client { Email = "jjwatt@cardinals", Name = "JJ Watt" });

            return testClients;
        }
    }
}
