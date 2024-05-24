using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace component_composition.Models
{
    [Table("Roles")]
    public class Role
    {
        public int RoleID { get; set; }
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
