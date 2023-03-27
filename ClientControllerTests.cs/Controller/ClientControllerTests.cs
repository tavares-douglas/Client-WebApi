using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechnicalCase_PBTech.Controllers;
using TechnicalCase_PBTech.Data;
using TechnicalCase_PBTech.Dto;
using TechnicalCase_PBTech.Interfaces;
using TechnicalCase_PBTech.Models;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace TechnicalCase_PBTech.Tests.Controllers
{
    [TestClass]
    public class ClientControllerTest
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        public ClientControllerTest()
        {
            _clientRepository = A.Fake<IClientRepository>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public void ClientController_GetClients_ReturnsOk()
        {
            var clients = A.Fake<ICollection<ClientDto>>();
            var clientList = A.Fake<List<ClientDto>>();
            A.CallTo(() => _mapper.Map<List<ClientDto>>(clients)).Returns(clientList);
            var controller = new ClientController(_clientRepository, _mapper);

            var result = controller.GetClients();

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void ClientController_GetClient_ReturnsOk()
        {
            var clientDto = A.Fake<ClientDto>();
            var clients = A.Fake<ICollection<ClientDto>>();
            var clientList = A.Fake<IList<ClientDto>>();
            var client = A.Fake<Client>();

            A.CallTo(() => _clientRepository.GetClientTrimToUpper(clientDto));
            A.CallTo(() => _mapper.Map<ClientDto>(client)).Returns(clientDto);
            A.CallTo(() => _clientRepository.CreateClient(client)).Returns(false);

            
            A.CallTo(() => _mapper.Map<ClientDto>(client)).Returns(clientDto);

            var controller = new ClientController(_clientRepository, _mapper);

            controller.CreateClient(clientDto);
            var result = controller.GetClient(client.Email);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [TestMethod]
        public void ClientController_CreateClient_ReturnsOk()
        {
            var clientDto = A.Fake<ClientDto>();
            var clients = A.Fake<ICollection<ClientDto>>();
            var clientList = A.Fake<IList<ClientDto>>();
            var client = A.Fake<Client>();

            A.CallTo(() => _clientRepository.GetClientTrimToUpper(clientDto));
            A.CallTo(() => _mapper.Map<Client>(clientDto)).Returns(client);
            A.CallTo(() => _clientRepository.CreateClient(client)).Returns(false);

            var controller = new ClientController(_clientRepository, _mapper);

            var result = controller.CreateClient(clientDto);

            result.Should().NotBeNull();
        }
    }
}
