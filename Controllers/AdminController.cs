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
public IActionResult Ekle(Urun urun, string renklerText)
{
    if (!string.IsNullOrWhiteSpace(renklerText))
    {
        urun.Renkler = renklerText
            .Split(',')
            .Select(parca => parca.Trim())
            .Where(parca => parca.Contains(':'))
            .Select(parca =>
            {
                var bilesenler = parca.Split(':');
                return new RenkSecenegi
                {
                    Ad = bilesenler[0].Trim(),
                    GorselYolu = bilesenler[1].Trim()
                };
            })
            .ToList();
    }

    _urunServisi.UrunEkle(urun);
    return RedirectToAction("Index", "Urun");
  }
 }
}