using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechnicalCase_PBTech.Models
{
    public class Client
    {
        [Key]
        public string Email { get; set; }
        public string Name { get; set; }

    }
}
