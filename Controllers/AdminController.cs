using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BayiUrunKatalogu.Models;
using BayiUrunKatalogu.Services;

namespace BayiUrunKatalogu.Controllers
{
 [Authorize(Roles = "Admin")]
 public class AdminController : Controller
 {
    private readonly UrunServisi _urunServisi;
    public AdminController(UrunServisi urunServisi)
    {
        _urunServisi = urunServisi;
    }
    [HttpGet]
    public IActionResult Ekle()
    {
        return View();
    }
     [HttpPost]
    public IActionResult Ekle(Urun urun)
    {
        _urunServisi.UrunEkle(urun);
        return RedirectToAction("Index","Urun");
    }
 }
}