using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TechnicalCase_PBTech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {   
        public static List<Client> clients = new List<Client>
        {
           new Client
           {
                Name = "Douglas",
                Email = "douglas@outlook.com"
           },
           new Client {
                Name = "Ana Julia",
                Email = "anajulia@outlook.com"
           }
        };
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
            var client = clients.Find(client => client.Email == Email);
            if (client == null)
                return BadRequest("Client not found.");
            return Ok(client);
        }

        [HttpPost]
        public async Task<ActionResult<List<Client>>> AddClient(Client client)
        {
            clients.Add(client);
            return Ok(clients);
        }

        [HttpPut]
        public async Task<ActionResult<List<Client>>> UpdateClient(Client request)
        {
            var client = clients.Find(client => client.Email == request.Email);
            if (client == null)
                return BadRequest("Client not found.");

            client.Name = request.Name;
            client.Email = request.Email;

            return Ok(client);
        }

        [HttpDelete("{Email}")]
        public async Task<ActionResult<List<Client>>> Delete(String Email)
        {
            var client = clients.Find(client => client.Email == Email);
            if (client == null)
                return BadRequest("Client not found.");

            clients.Remove(client);
            return Ok(clients);
        }



        }
}
