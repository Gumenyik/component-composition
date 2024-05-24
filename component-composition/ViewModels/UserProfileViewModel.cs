namespace component_composition.ViewModels
{
    using System.Collections.Generic;
    using component_composition.Models;
    public class UserProfileViewModel
    {
        public User User { get; set; }
        public List<UserHistory> UserHistories { get; set; }
    }
}
