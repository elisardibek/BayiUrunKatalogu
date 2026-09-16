namespace BayiUrunKatalogu.Models
{
    public static class RenkYardimcisi
    {
       private static readonly Dictionary<string, string> RenkKodlari = new()
{
    { "siyah", "#000000" },
    { "beyaz", "#F0EBE0" },
    { "kar beyazi", "#FFFFFF" },
    { "bordo", "#800020" },
    { "yesil", "#2A9D8F" },
    { "yeşil", "#2A9D8F" },
    { "mavi", "#457B9D" },
    { "sari", "#F4A261" },
    { "sarı", "#F4A261" },
    { "pembe", "#FF2E9F" },
    { "gri", "#8D99AE" },
    { "kahverengi", "#6F4E37" },
    { "turuncu", "#F77F00" },
    { "mor", "#7B2CBF" },
    { "lacivert", "#1D3557" },
    { "kirmizi", "#E63946" },
};

        public static string Kod(string renkAdi)
        {
            if (string.IsNullOrWhiteSpace(renkAdi))
                return "#555555";

            var anahtar = renkAdi.Trim().ToLower();
            return RenkKodlari.ContainsKey(anahtar) ? RenkKodlari[anahtar] : "#555555";
        }
    }
}