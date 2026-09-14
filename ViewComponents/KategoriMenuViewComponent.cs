using Microsoft.AspNetCore.Mvc;
using BayiUrunKatalogu.Services;

namespace BayiUrunKatalogu.ViewComponents
{
    public class KategoriMenuViewComponent : ViewComponent
    {
        private readonly UrunServisi _urunServisi;
        public KategoriMenuViewComponent(UrunServisi urunServisi)
        {
            _urunServisi= urunServisi;
        }
        public IViewComponentResult Invoke()
        {
            var kategoriler = _urunServisi.TumKategorileriGetir();
            return View(kategoriler);
        }
    }
}