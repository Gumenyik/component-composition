using Microsoft.AspNetCore.Mvc;
using component_composition.Models;

public class ProfileController : Controller
{
    private readonly UserContext _context;

    public ProfileController(UserContext context)
    {
        _context = context;
    }

    public IActionResult Index(int id)
    {
        User user = _context.Users.Find(id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(User user)
    {
        if (ModelState.IsValid)
        {
            _context.Update(user);
            _context.SaveChanges();
            return RedirectToAction("Index", new { id = user.UserID });
        }
        return View(user);
    }
}
