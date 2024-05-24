using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace component_composition.Models
{
    [Table("Aggregate")]
    public class Aggregate
    {
        [Key]
        public int AggregateID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public int StateID { get; set; }

        [ForeignKey("StateID")]
        public State State { get; set; }
    }
}
