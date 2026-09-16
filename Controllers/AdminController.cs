using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BayiUrunKatalogu.Models;
using BayiUrunKatalogu.Services;
using ClosedXML.Excel;

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
  [HttpGet]
  public IActionResult Duzenle(int id)
  {
      var urun = _urunServisi.UrunGetirById(id);
      if (urun == null)
          return NotFound();

      return View(urun);
  }
[HttpGet]
public IActionResult ExcelAktar()
{
    var urunler = _urunServisi.TumUrunleriGetir();

    using var workbook = new XLWorkbook();
    var worksheet = workbook.Worksheets.Add("Urunler");

    // Başlıklar
    worksheet.Cell(1, 1).Value = "Id";
    worksheet.Cell(1, 2).Value = "Urun Adi";
    worksheet.Cell(1, 3).Value = "Urun Kodu";
    worksheet.Cell(1, 4).Value = "Kategori";
    worksheet.Cell(1, 5).Value = "Marka";
    worksheet.Cell(1, 6).Value = "Fiyat";
    worksheet.Cell(1, 7).Value = "Stok Adedi";
    worksheet.Cell(1, 8).Value = "Renkler";

    var basliklar = worksheet.Range(1, 1, 1, 8);
    basliklar.Style.Font.Bold = true;
    basliklar.Style.Fill.BackgroundColor = XLColor.FromHtml("#1F4E78");
    basliklar.Style.Font.FontColor = XLColor.White;

    // Veriler
    int satir = 2;
    foreach (var urun in urunler)
    {
        worksheet.Cell(satir, 1).Value = urun.Id;
        worksheet.Cell(satir, 2).Value = urun.UrunAdi;
        worksheet.Cell(satir, 3).Value = urun.UrunKodu;
        worksheet.Cell(satir, 4).Value = urun.Kategori;
        worksheet.Cell(satir, 5).Value = urun.Marka;
        worksheet.Cell(satir, 6).Value = urun.Fiyat;
        worksheet.Cell(satir, 7).Value = urun.StokAdedi;
        worksheet.Cell(satir, 8).Value = urun.Renkler != null && urun.Renkler.Count > 0
            ? string.Join(", ", urun.Renkler.Select(r => r.Ad))
            : "-";
        satir++;
    }

    worksheet.Columns().AdjustToContents();

    using var stream = new MemoryStream();
    workbook.SaveAs(stream);
    var icerik = stream.ToArray();

    return File(icerik,
        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        "Bayi_Urun_Katalogu.xlsx");
}
[HttpPost]
public IActionResult Sil(int id)
{
    _urunServisi.UrunSil(id);
    return RedirectToAction("Index", "Urun");
}
  [HttpPost]
  public IActionResult Duzenle(Urun urun, string renklerText)
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

      _urunServisi.UrunGuncelle(urun);
      return RedirectToAction("Index", "Urun");
  }
 }
}