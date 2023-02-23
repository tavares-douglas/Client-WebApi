using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace TechnicalCase_PBTech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {   
        private readonly DataContext _context;
 
        public ClientController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            return Ok(await _context.Clients.ToListAsync());
        }

        [HttpGet("{Email}")]
        public async Task<ActionResult<List<Client>>> GetClient(String Email)
        {
            var client = await _context.Clients.FindAsync(Email);
            if (client == null)
                return BadRequest("Client not found.");
            return Ok(client);
        }

        [HttpPost]
        public async Task<ActionResult<List<Client>>> AddClient(Client client)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(client.Email);
            if (!match.Success)
                return BadRequest("E-mail not allowed.");

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return Ok(await _context.Clients.ToListAsync());
        }

        [HttpPut("{Email}")]
        public async Task<ActionResult<List<Client>>> UpdateClient(String Email, Client request)
        {
            var client = await _context.Clients.FindAsync(Email);
            if (client == null)
                return BadRequest("Client not found.");

            await DeleteClient(Email);
            await AddClient(request);

            return Ok(await _context.Clients.ToListAsync());
        }

        [HttpDelete("{Email}")]
        public async Task<ActionResult<List<Client>>> DeleteClient(String Email)
        {
            var client = await _context.Clients.FindAsync(Email);
            if (client == null)
                return BadRequest("Client not found.");

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return Ok(await _context.Clients.ToListAsync());
        }

        }
}
