using TechnicalCase_PBTech.Models;

namespace TechnicalCase_PBTech.Interfaces
{
    public interface IClientRepository
    {
        ICollection<Client> GetClients();
        Client GetClient(string Email);
    }
}
