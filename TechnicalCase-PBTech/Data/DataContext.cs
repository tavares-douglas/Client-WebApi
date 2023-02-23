using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TechnicalCase_PBTech.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){ }

        public DbSet<Client> Clients { get; set; }
    }
}
