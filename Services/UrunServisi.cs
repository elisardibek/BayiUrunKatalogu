using BayiUrunKatalogu.Models;
using System.Text.Json;

namespace BayiUrunKatalogu.Services
{
           public class UrunServisi{
           private readonly string _dosyaYolu= "Data/Urunler.json";

            public List<Urun> TumUrunleriGetir()
            {
            if(!File.Exists(_dosyaYolu))
             return new List<Urun>();    

            string json= File.ReadAllText(_dosyaYolu);
            return JsonSerializer.Deserialize<List<Urun>>(json) ?? new List<Urun>();   
            }

            public void UrunEkle(Urun yeniUrun)
            {
            var Urunler = TumUrunleriGetir();
            yeniUrun.Id= Urunler.Count > 0 ? Urunler.Max(k=> k.Id) +1 : 1;
            Urunler.Add(yeniUrun);

            string json=JsonSerializer.Serialize(Urunler, new JsonSerializerOptions {WriteIndented = true});
            File.WriteAllText(_dosyaYolu, json);
            }

            public Urun UrunGetirById(int id)
            {
                var urunler = TumUrunleriGetir();
                return urunler.FirstOrDefault(u => u.Id == id);
            }

            public List<Urun> Ara(string aranan)
            {
                var urunler = TumUrunleriGetir();
                return urunler
                    .Where(u => (u.UrunAdi != null && u.UrunAdi.Contains(aranan, StringComparison.OrdinalIgnoreCase))
                             || (u.Kategori != null && u.Kategori.Contains(aranan, StringComparison.OrdinalIgnoreCase)))
                    .ToList();
            }
            public List<Urun> KategoriyeGoreGetir(string kategori)
{
    var urunler = TumUrunleriGetir();

    if (string.IsNullOrEmpty(kategori))
        return urunler;

    return urunler
        .Where(u => u.Kategori != null && u.Kategori.Equals(kategori, StringComparison.OrdinalIgnoreCase))
                    .ToList();
}

 public List<string> TumKategorileriGetir()
  {
    var urunler = TumUrunleriGetir();
    return urunler
        .Where(u => u.Kategori != null)
                    .Select(u => u.Kategori)
                    .Distinct()
                    .ToList();
   } 
 }
}