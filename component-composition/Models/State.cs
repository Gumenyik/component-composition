using System.ComponentModel.DataAnnotations.Schema;

namespace component_composition.Models
{
    [Table("State")]
    public class State
    {
        public int StateID { get; set; }
        public string Name { get; set; }
    }
}
