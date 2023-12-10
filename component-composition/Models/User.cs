using System.ComponentModel.DataAnnotations.Schema;

namespace component_composition.Models
{
    [Table("Users")]
    public class User
    {
        public int UserID { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int RoleID { get; set; }
        public int RootID { get; set; }
    }
}
