using TechnicalCase_PBTech.Dto;
using TechnicalCase_PBTech.Models;

namespace TechnicalCase_PBTech.Interfaces
{
    public interface IClientRepository
    {
        ICollection<Client> GetClients();
        Client GetClient(string Email);
        bool ClientExists(string Email);
        bool CreateClient(Client client);
        bool UpdateClient(Client client);
        bool DeleteClient(Client client);
        bool Save();
        bool ValidateEmail(string Email);
        Client GetClientTrimToUpper(ClientDto clientDto);
    }
}
