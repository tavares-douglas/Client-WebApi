using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TechnicalCase_PBTech.Dto;
using TechnicalCase_PBTech.Interfaces;
using TechnicalCase_PBTech.Models;

namespace TechnicalCase_PBTech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : Controller
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientController(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Client>))]
        public IActionResult GetClients()
        {
            var clients = _mapper.Map<List<ClientDto>>(_clientRepository.GetClients());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(clients);
        }

        [HttpGet("{Email}")]
        [ProducesResponseType(200, Type = typeof(Client))]
        [ProducesResponseType(400)]
        public IActionResult GetClient(String Email)
        {
            if (!_clientRepository.ClientExists(Email)) return NotFound();

            var client = _mapper.Map<ClientDto>(_clientRepository.GetClient(Email));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(client);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateClient([FromBody] ClientDto clientCreate)
        {
            if (clientCreate == null) return BadRequest(ModelState);

            var clients = _clientRepository.GetClientTrimToUpper(clientCreate);
     
            if(clients != null) return StatusCode(422, "Client already exists");

            if(!_clientRepository.ValidateEmail(clientCreate.Email)) return StatusCode(415, "E-mail not allowed.");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var clientMap = _mapper.Map<Client>(clientCreate);
            if(!_clientRepository.CreateClient(clientMap)) return StatusCode(500, "Something went wrong while saving.");

            return Ok("Successfully created.");
        }

        [HttpPut("{Email}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateClient(string Email, [FromBody]ClientDto updatedClient)
        {
            if (!_clientRepository.ClientExists(Email)) return NotFound();

            if(!_clientRepository.ValidateEmail(updatedClient.Email)) return StatusCode(415, "E-mail not allowed.");

            DeleteClient(Email);
            CreateClient(updatedClient);

            return NoContent();
        }

        [HttpDelete("{Email}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteClient(String Email)
        {
            if (!_clientRepository.ClientExists(Email)) return NotFound();

            var clientToBeDeleted = _clientRepository.GetClient(Email);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!_clientRepository.DeleteClient(clientToBeDeleted)) ModelState.AddModelError("", "Something went wrong.");

            return NoContent();
        }

    }
}