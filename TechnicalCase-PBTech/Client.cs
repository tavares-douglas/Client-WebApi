using System.ComponentModel.DataAnnotations;

namespace TechnicalCase_PBTech
{
    public class Client
    {
        [Key]
        public String Email { get; set; } = string.Empty;
        public String Name { get; set; } = string.Empty;
        
    }
}
