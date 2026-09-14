using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BayiUrunKatalogu.Services;

namespace BayiUrunKatalogu.Controllers
{
    public class AccountController : Controller
    {
        private readonly KullaniciServisi _kullaniciServisi;
        public AccountController(KullaniciServisi kullaniciServisi)
        {
            _kullaniciServisi = kullaniciServisi;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string kullaniciAdi, string sifre)
        {
            var kullanici = _kullaniciServisi.GirisYap(kullaniciAdi, sifre);

            if (kullanici == null)
            {
                ViewBag.Hata="Kullanıcı adı veya şifre hatalı.";
                return View();
            }
            var claims= new List <Claim>
            {
                new Claim(ClaimTypes.Name, kullanici.KullaniciAdi),
                new Claim(ClaimTypes.Role, kullanici.Rol.ToString())
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme );
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        
        return RedirectToAction("Index","Urun");
        
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","Urun");
        }
    }
}