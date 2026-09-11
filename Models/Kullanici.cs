namespace BayiUrunKatalogu.Models
{
    public class Kullanici
    {
        public int Id { get; set; }
        public string KullaniciAdi { get; set; }
        public string SifreHash { get; set; }   // artık düz şifre değil, hash tutuyoruz
        public KullaniciRolu Rol { get; set; }
    }
}