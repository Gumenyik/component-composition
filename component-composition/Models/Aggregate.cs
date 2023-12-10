using System.ComponentModel.DataAnnotations.Schema;

namespace component_composition.Models
{
    [Table("Aggregate")]
    public class Aggregate
    {
        public int AggregateID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public int StateID { get; set; }
        public State State { get; set; }


    }
}
