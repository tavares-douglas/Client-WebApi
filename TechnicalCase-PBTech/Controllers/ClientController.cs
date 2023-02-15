using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<List<Client>>> Get()
        {
            return Ok(await _context.Clients.ToListAsync());
        }

        [HttpGet("{Email}")]
        public async Task<ActionResult<List<Client>>> Get(String Email)
        {
            var client = await _context.Clients.FindAsync(Email);
            if (client == null)
                return BadRequest("Client not found.");
            return Ok(client);
        }

        [HttpPost]
        public async Task<ActionResult<List<Client>>> AddClient(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return Ok(await _context.Clients.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Client>>> UpdateClient(Client request)
        {
            var client = await _context.Clients.FindAsync(request.Email);
            if (client == null)
                return BadRequest("Client not found.");

            client.Name = request.Name;
            client.Email = request.Email;

            await _context.SaveChangesAsync();

            return Ok(await _context.Clients.ToListAsync());
        }

        [HttpDelete("{Email}")]
        public async Task<ActionResult<List<Client>>> Delete(String Email)
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
