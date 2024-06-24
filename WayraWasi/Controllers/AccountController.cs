using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WayraWasi.ViewModels;

namespace WayraWasi.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager; // Manejo en el inicio de sesion del programa.
        private readonly UserManager<IdentityUser> _userManager; // Manejo a la hora de registrar un usuario en programa
        private readonly RoleManager<IdentityRole> _rolUser; // Manejo de roles en el programa

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> rolUser)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _rolUser = rolUser;
        }

        /* Solo se le mostrara una pagina para logearse en la cual tambien podra registrase */

        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(RegisterViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                var usuario = new IdentityUser { UserName = modelo.Email, Email = modelo.Email };
                var resultado = await _userManager.CreateAsync(usuario, modelo.Password);

                if (resultado.Succeeded)
                {
                    if (!await _rolUser.RoleExistsAsync("Administrador")) // Veo si existe un usuario admin sino lo agrego
                    {
                        var Admin = new IdentityRole("Administrador");
                        await _rolUser.CreateAsync(Admin);
                    }
                    if(_userManager.Users.Count() == 1) // Solo un usuario es el Admin, donde sea el primero que nosotros creemos
                    {
                        await _userManager.AddToRoleAsync(usuario, "Administrador"); // Asigno el Rol administrador a mi usuario
                    }

                    await _signInManager.SignInAsync(usuario, isPersistent: false);             
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(modelo);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel modelo, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var resultado = await _signInManager.PasswordSignInAsync(modelo.Email, modelo.Password, modelo.RememberMe, lockoutOnFailure: false);
                if (resultado.Succeeded)
                {
                    return Redireccionar(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, " Su email o su contraseña son incorrectos.");
                    return View(modelo);
                }
            }
            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        public IActionResult AccessDenied(string returnUrl)
        {
            return RedirectToAction("Privacy", "Home");
        }

        private IActionResult Redireccionar(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}

