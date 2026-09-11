using BayiUrunKatalogu.Models;
using System.Text.Json;

namespace BayiUrunKatalogu.Services
{
    public class KullaniciServisi{
        private readonly string _dosyaYolu= "Data/kullanicilar.json";

        public List<Kullanici> TumKullanicilariGetir(){
            if(!File.Exists(_dosyaYolu))
             return new List<Kullanici>();    

            string json= File.ReadAllText(_dosyaYolu);
            return JsonSerializer.Deserialize<List<Kullanici>>(json) ?? new List<Kullanici>();   
            }

            public void KullaniciEkle(Kullanici yeniKullanici){
            var kullanicilar = TumKullanicilariGetir();
            yeniKullanici.Id= kullanicilar.Count > 0 ? kullanicilar.Max(k=> k.Id) +1 : 1;
            kullanicilar.Add(yeniKullanici);

            string json=JsonSerializer.Serialize(kullanicilar, new JsonSerializerOptions {WriteIndented = true});
            File.WriteAllText(_dosyaYolu, json);
            }

            public Kullanici GirisYap(string kullaniciAdi, string sifre){
              var kullanicilar = TumKullanicilariGetir();
              var kullanici = kullanicilar.FirstOrDefault(k => k.KullaniciAdi == kullaniciAdi);

              if (kullanici == null)
              return null;

              bool dogruMu= BCrypt.Net.BCrypt.Verify(sifre, kullanici.SifreHash);
              return dogruMu ? kullanici : null;

            }
    }
}