using TechnicalCase_PBTech.Data;
using TechnicalCase_PBTech.Models;
using System.Diagnostics.Metrics;

namespace TechnicalCase_PBTech
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.Clients.Any())
            {
                var clients = new List<Client>()
                {
                    new Client()
                    {
                        Email = "douglas@outlook.com",
                        Name = "Douglas"
                    },
                    new Client()
                    {
                        Email = "anajulia@outlook.com",
                        Name = "Ana Julia"
                    },
                    new Client()
                    {
                        Email = "felipe@outlook.com",
                        Name = "Felipe"
                    },
                    new Client()
                    {
                        Email = "jorge@outlook.com",
                        Name = "Jorge"
                    },
                };
                dataContext.Clients.AddRange(clients);
                dataContext.SaveChanges();
            }
        }
    }
}