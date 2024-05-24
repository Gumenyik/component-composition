using component_composition.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Diagnostics.Eventing.Reader;
using component_composition.ViewModels;
using System.Data.Entity.Core.Common.CommandTrees;
using component_composition.Services;

namespace component_composition.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CatalogContext _context;
        private readonly UserHistoryService _userHistoryService;

        public HomeController(ILogger<HomeController> logger, CatalogContext context, UserHistoryService userHistoryService)
        {
            _userHistoryService = userHistoryService;
            _logger = logger;
            _context = context;
        }
        [Authorize]
        public IActionResult Index()
        {
            var aggregates = _context.Aggregates.Include(a => a.State).ToList();
            ViewBag.States = _context.States.ToList();
            return View(aggregates);
        }
        [Authorize]
        public IActionResult RepairmanList()
        {
            var users = _context.Users
                        .Include(u => u.Status)
                        .ToList();
            var aggregates = _context.Aggregates
                            .Where(s => s.StateID == 3)
                            .ToList();

            var viewModel = new RepairmanTaskViewModel
            {
                Users = users,
                Aggregates = aggregates
            };

            ViewBag.Status = _context.Status.ToList();
            ViewBag.Aggregate_Name = _context.Aggregates.ToList(); ; 

            return View(viewModel); 
        }



        [Authorize]
        [HttpPost]
        public async Task<IActionResult> GiveTask(int UserID, int AggregateID)
        {
            var user = await _context.Users.FindAsync(UserID);
            if (user == null)
            {
                return NotFound("Користувача не знайдено.");
            }

            var aggregate = await _context.Aggregates.FindAsync(AggregateID);
            if (aggregate == null)
            {
                return NotFound("Агрегат не знайдено.");
            }
            aggregate.StateID = 4;

            user.AggregateID = AggregateID;
            user.StatusID = 3;

            string currentUserEmail = User.Identity.Name;
            var userC = _context.Users.FirstOrDefault(u => u.Email == currentUserEmail);
            string hist = $"Користувач  {userC.Name} дав завдання користувачу {user.Name}";
            await _userHistoryService.LogAction(userC.UserID, hist);

            await _context.SaveChangesAsync();

            return RedirectToAction("RepairmanList");
        }

        [Authorize]
        [HttpGet]
        public IActionResult CreateAggregate()
        {  
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAggregate(NewAggregateModel model, IFormFile image)
        {
            Aggregate agr = await _context.Aggregates.FirstOrDefaultAsync(u => u.Name == model.Name);
            if (agr == null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(image.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)image.Length);
                }

                _context.Aggregates.Add(new Aggregate
                {
                    Name = model.Name,
                    Description = model.Description,
                    Image = imageData, 
                    StateID = 1,
                });
                string currentUserEmail = User.Identity.Name;
                var user = _context.Users.FirstOrDefault(u => u.Email == currentUserEmail);
                
                string hist = "Користувач створив новий агрегат " + model.Name;
                await _userHistoryService.LogAction(user.UserID, hist);
                
                await _context.SaveChangesAsync();
                
                return RedirectToAction("Index", "Home");

            }
            else
            {
                ModelState.AddModelError("", "Такий об`єкт можливо вже існує");
            }
    
            return View(model);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Profile(int id)
        {
            var user = await _context.Users
                .Include(u => u.Status)
                .FirstOrDefaultAsync(u => u.UserID == id);

            if (user == null)
            {
                return NotFound();
            }

            var userHistories = await _context.UserHistories
                .Where(uh => uh.UserID == id)
                .OrderByDescending(uh => uh.ActionDate)
                .ToListAsync();

            var profileViewModel = new UserProfileViewModel
            {
                User = user,
                UserHistories = userHistories
            };

            ViewBag.Status = await _context.Status.ToListAsync();

            return View(profileViewModel);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync("Cookies");
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> ChangeState(int aggregateId, int newStateId)
        {
            var aggregate = _context.Aggregates.Find(aggregateId);
            var newState = _context.States.Find(newStateId);

            if (aggregate == null)
            {
                return NotFound("Aggregate with id " + aggregateId + " not found.");
            }

            if (newState == null)
            {
                return NotFound("State with id " + newStateId + " not found.");
            }

            string currentUserEmail = User.Identity.Name;
            var user = _context.Users.FirstOrDefault(u => u.Email == currentUserEmail);

            if (user != null)
            {
 
                aggregate.State = newState;

                string hist = $"Користувач змінив стан {aggregate.Name} на {aggregate.State.Name}";
                await _userHistoryService.LogAction(user.UserID, hist);

               
                if(newStateId == 4)
                {
                    user.AggregateID = aggregateId;
                    user.StatusID = 3;
                }
                else if(newStateId == 5)
                {
                    user.StatusID = 1;
                    user.AggregateID = 0;
                }
               
            }
            
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
