using TechnicalCase_PBTech.Interfaces;
using TechnicalCase_PBTech.Models;

namespace TechnicalCase_PBTech.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly DataContext _context;

        public ClientRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Client> GetClients()
        {
            return _context.Clients.OrderBy(c => c.Email).ToList();
        }

        public Client GetClient(string email)
        {
            return _context.Clients.Where(c => c.Email == email).FirstOrDefault();
        }
    }
}
