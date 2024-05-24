using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace component_composition.Models
{
    [Table("Users")]
    public class User
    {
        public int UserID { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public int RoleID { get; set; }

        public Role Role { get; set; }

        public int StatusID { get; set; }

        [ForeignKey("StatusID")]
        public Status Status { get; set; }

        public int AggregateID { get; set; }

        [ForeignKey("AggregateID")]
        public Aggregate Aggregate { get; set; }

    }
}