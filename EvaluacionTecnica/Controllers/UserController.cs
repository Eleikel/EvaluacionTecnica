using EvaluacionTecnica.Business.Interfaces.Service;
using EvaluacionTecnica.Business.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvaluacionTecnica.Presentation.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public UserController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        public async Task<IActionResult> Index(string search = "", int page = 1, int pageSize = 5)
         {
            var users = await _userService.GetAll();

            if (!string.IsNullOrEmpty(search))
                users = users.Where(u =>
                    u.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    u.LastName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    u.UserName.Contains(search, StringComparison.OrdinalIgnoreCase));

            var totalItems = users.Count();
            var pagedUsers = users
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.Search = search;
            ViewBag.Page = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            return View(pagedUsers);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Roles = await _roleService.GetAll();
            return View(nameof(Create), new UserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = await _roleService.GetAll();
                return View(nameof(Create), model);
            }
            model.Birthday = model.Birthday ?? new DateTime(1753, 1, 1);


            await _userService.Create(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userService.GetById(id);
            if (user == null) return NotFound();

            ViewBag.Roles = await _roleService.GetAll();
            return View(nameof(Create), user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = await _roleService.GetAll();
                return View(nameof(Create), model);
            }

            await _userService.Update(id, model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetById(id);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _userService.Delete(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
