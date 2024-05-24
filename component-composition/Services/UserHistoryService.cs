using component_composition.Models;

namespace component_composition.Services
{
    public class UserHistoryService
    {
        private readonly UserContext _context;

        public UserHistoryService(UserContext context)
        {
            _context = context;
        }

        public async Task LogAction(int userId, string action)
        {
            var userHistory = new UserHistory
            {
                UserID = userId,
                Action = action,
                ActionDate = DateTime.Now
            };

            _context.UserHistories.Add(userHistory);
            await _context.SaveChangesAsync();
        }
    }
}
