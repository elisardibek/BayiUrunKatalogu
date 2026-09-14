using Microsoft.AspNetCore.Mvc;
using BayiUrunKatalogu.Services;

namespace BayiUrunKatalogu.Controllers
{
    public class UrunController : Controller
    {
        private readonly UrunServisi _urunServisi;
        public UrunController(UrunServisi urunServisi)
        {
            _urunServisi = urunServisi;
        }
        public IActionResult Index()
        {
            var urunler = _urunServisi.TumUrunleriGetir();
            return View(urunler);
        }
        public IActionResult Ara(string q)
        {
            var sonuclar =_urunServisi.Ara(q);
            return View("Index", sonuclar);
        }
        public IActionResult Kategori(string kategori)
{
    var sonuclar = _urunServisi.KategoriyeGoreGetir(kategori);
    return View("Index", sonuclar);
}
        public IActionResult Detay(int id)
        {
            var urun =_urunServisi.UrunGetirById(id);
            if (urun==null)
            return NotFound();
            return View(urun);
        
        }
    }
}