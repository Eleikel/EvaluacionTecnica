using EvaluacionTecnica.Business.ViewModels.Roles;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using EvaluacionTecnica.Business.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;

namespace EvaluacionTecnica.Presentation.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Index()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "User");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Login(AuthViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Datos inválidos" });

            var userViewModel = await _authService.LoginAsync(model);

            if (userViewModel == null)
                return Json(new { success = false, message = "Credenciales incorrectas" });

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userViewModel.Id.ToString()),
            new Claim(ClaimTypes.Name, userViewModel.UserName ??  $"{userViewModel.Name} {userViewModel.LastName}" ?? "user")

        };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8) //expiracion
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

            return Json(new { success = true });
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Auth");
        }
    }
}

