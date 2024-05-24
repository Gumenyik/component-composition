using System.ComponentModel.DataAnnotations.Schema;

namespace component_composition.Models
{
    [Table("UserHistory")]
    public class UserHistory
    {
        public int UserHistoryID { get; set; }

        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        public string Action { get; set; }

        public DateTime ActionDate { get; set; }
    }
}
