using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RelaxKundura3.Abstract;

namespace RelaxKundura3.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminController : Controller
    {
        
        private readonly UserManager<IdentityUser> _userManager;
        
        public AdminController (UserManager<IdentityUser> userManager)
        {
            
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var userList = _userManager.Users.ToList();

            return View(userList);
        }

        [HttpPost]
        public async Task<IActionResult> Sil(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error deleting user.");
                return View("Index", _userManager.Users.ToList());
            }
        }
    }
}
