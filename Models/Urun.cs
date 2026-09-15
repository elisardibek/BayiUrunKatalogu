namespace BayiUrunKatalogu.Models
{
public class Urun
{
    public int Id { get; set; }
    public string UrunAdi { get; set; }
    public string UrunKodu { get; set; }
    public string Aciklama { get; set; }
    public string Kategori { get; set; }
    public string Marka { get; set; }
    public decimal Fiyat { get; set; }
    public int StokAdedi { get; set; }
    public string GorselYolu { get; set; }
    public List<RenkSecenegi> Renkler { get; set; } = new List<RenkSecenegi>();
}
}