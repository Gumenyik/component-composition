using component_composition.Models;

namespace component_composition.ViewModels
{
    public class RepairmanTaskViewModel
    {
        public List<User> Users { get; set; }
        public List<Aggregate> Aggregates  { get; set; }
    }
}
