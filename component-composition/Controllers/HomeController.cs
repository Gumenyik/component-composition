using component_composition.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace component_composition.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CatalogContext _context;

        public HomeController(ILogger<HomeController> logger, CatalogContext context)
        {
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

        public IActionResult Privacy()
        {
            return View();
        }

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
        public IActionResult ChangeState(int aggregateId, int newStateId)
        {
            var aggregate = _context.Aggregates.Find(aggregateId);
            if (aggregate == null)
            {
                return NotFound(); 
            }

            var newState = _context.States.Find(newStateId);
            if (newState == null)
            {
                return NotFound(); 
            }

            aggregate.State = newState;
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
