using EvaluacionTecnica.Business.Interfaces.Service;
using EvaluacionTecnica.Business.ViewModels.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvaluacionTecnica.Presentation.Controllers
{

    [Authorize]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<IActionResult> Index(string search = "", int page = 1, int pageSize = 5)
        {
            var roles = await _roleService.GetAll();

            if (!string.IsNullOrEmpty(search))
                roles = roles.Where(r => r.Name.Contains(search, StringComparison.OrdinalIgnoreCase));

            var totalItems = roles.Count();
            var pagedRoles = roles.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.Search = search;
            ViewBag.Page = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            return View(pagedRoles);
        }

        public IActionResult Create() => View("Create", new RoleViewModel());

        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (!ModelState.IsValid)
                return View(nameof(Create), model);

            await _roleService.Create(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var role = await _roleService.GetById(id);
            if (role == null) return NotFound();

            return View(nameof(Create), role);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, RoleViewModel model)
        {
            if (!ModelState.IsValid)
                return View(nameof(Create), model);

            await _roleService.Update(id, model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var role = await _roleService.GetById(id);
            if (role == null) return NotFound();

            return View(role);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _roleService.Delete(id);

            }
            catch (InvalidOperationException ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
