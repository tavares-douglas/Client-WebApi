using System.Text.RegularExpressions;
using TechnicalCase_PBTech.Dto;
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

        public bool ClientExists(string Email)
        {
            return _context.Clients.Any(c => c.Email == Email);
        }

        public bool CreateClient(Client client)
        {
            _context.Add(client);
            return Save();
        }

        public bool UpdateClient(Client client)
        {
            _context.Update(client);
            return Save();
        }

        public bool DeleteClient(Client client)
        {
            _context.Remove(client);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        } 

        public bool ValidateEmail(string Email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(Email);
            if (!match.Success) return false;
            return true;
        }

        public Client GetClientTrimToUpper(ClientDto clientDto)
        {
            return GetClients().Where(c => c.Email.Trim().ToUpper() == clientDto.Email.TrimEnd().ToUpper()).FirstOrDefault();
        }
    }
}
