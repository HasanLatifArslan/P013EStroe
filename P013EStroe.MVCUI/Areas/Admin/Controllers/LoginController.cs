using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using P013EStore.Core.Entities;
using P013EStroe.MVCUI.Models;
using P013EStroe.Service.Abstract;
using System.Security.Claims;

namespace P013EStroe.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly IService<AppUser> _service;

        public LoginController(IService<AppUser> service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Route("Logout")]
		public async Task<IActionResult> Logout()
		{
            await HttpContext.SignOutAsync(); //sistemden çıkış yap
            return Redirect("/Admin/Login"); // tekrardan giriş sayfasına yönlendir
		}
		[HttpPost]
        public async Task<IActionResult> Index(AdminLoginViewModel admin)
        {
            try
            {
                var kullanici = await _service.GetAsync(k => k.IsActive && k.IsAdmin && k.Email == admin.Email && k.Password == admin.Password);
                if (kullanici != null)
                {
                    var kullaniciYetkileri = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, kullanici.Email),
                        new Claim("Role", kullanici.IsAdmin ? "Admin" : "User"),
                        new Claim("UserGuid", kullanici.UserGuid.ToString())
                    };
                    var kullaniciKimliği = new ClaimsIdentity(kullaniciYetkileri, "Login");
                    ClaimsPrincipal principal = new(kullaniciKimliği);
                    await HttpContext.SignInAsync(principal); // httpcontext .net içerisinde geliyor ve uygulmanın çalışma anındaki içeriğe ulaşmamızı sağlıyor sıgnınasync metodu da .net içerisinde mevcut login işlemi yapmak istersek buradaki gibi kullanıyoruz.
                    return Redirect("/Admin/Main");
                }
                else
               
					ModelState.AddModelError("", "Giriş Başarısız ");
				
            }
            catch (Exception hata)
            {
                ModelState.AddModelError("", "Hata Oluştu! " + hata.Message);
                
            }
            return View();
        }
    }
}
