# SneaksDown — Bayi Ürün Kataloğu

ASP.NET Core MVC ile geliştirilmiş, rol bazlı yetkilendirme sistemine sahip bir **bayi ürün kataloğu** uygulaması. Bayiler ürünleri görüntüleyip kategori/isim bazlı arama yapabilir; Admin rolündeki kullanıcılar ürün ekleme, düzenleme, silme ve Excel'e aktarma gibi yönetimsel işlemleri gerçekleştirebilir.

Bu proje bir ders ödevi / staj kapsamında geliştirilmiştir. Sitedeki ürün, marka ve görseller tamamen kurgusaldır (gerçek markaların esprili birer parodisidir — örn. *Nayki*, *Adisad*, *Pear Watch*) ve gerçek markalarla hiçbir bağlantısı yoktur.

---

## İçindekiler

- [Özellikler](#özellikler)
- [Kullanılan Teknolojiler](#kullanılan-teknolojiler)
- [Proje Yapısı](#proje-yapısı)
- [Kurulum ve Çalıştırma](#kurulum-ve-çalıştırma)
- [Kullanıcı Rolleri](#kullanıcı-rolleri)
- [Veri Modeli](#veri-modeli)
- [Ekran Görüntüleri](#ekran-görüntüleri)
- [Geliştirme Notları](#geliştirme-notları)
- [Olası Geliştirmeler](#olası-geliştirmeler)

---

## Özellikler

### Genel Kullanıcı (Bayi)
- Rol bazlı giriş / çıkış sistemi (cookie tabanlı kimlik doğrulama)
- Ürünleri listeleme, ürün adı veya kategoriye göre arama
- Kategori menüsünden filtreleme (Elektronik, Giyim, Fitness, Ev Yaşam)
- Ürün kartlarında renk seçeneklerine tıklayınca görselin anlık değişmesi
- Ürün detay sayfası: büyük görsel, stok durumu rozeti (Stokta / Stokta Yok), marka, ürün kodu, açıklama, beden etiketleri (hover'da renk değişimi)
- Gizlilik Politikası sayfası

### Admin Paneli
- Yeni ürün ekleme — kategoriye göre otomatik ürün kodu üretimi (örn. `TEK`, `GYM`, `SPR`, `EVK` önekleri + sıralı numara)
- Mevcut ürünü düzenleme (form, ürünün güncel bilgileriyle otomatik doluyor)
- Ürün silme (yanlışlıkla tıklamayı önlemek için onay penceresi ile)
- Tüm ürün kataloğunu tek tıkla Excel (.xlsx) dosyası olarak indirme (ClosedXML ile sunucu tarafında oluşturulup indirilir, diske kaydedilmez)

### Sistem / Altyapı
- Veri katmanı tamamen JSON dosyaları üzerinden çalışır (`Data/Urunler.json`, `Data/Kullanicilar.json`) — harici veritabanı yok
- Rota bulunamadığında (örn. var olmayan ürün ID'si) sitenin temasına uygun özel 404 sayfası gösterilir
- Beklenmeyen sunucu hatalarında ayrı bir hata sayfası (`Error.cshtml`) devreye girer

---

## Kullanılan Teknolojiler

| Katman | Teknoloji |
|---|---|
| Framework | ASP.NET Core MVC (.NET 9) |
| Dil | C# |
| Kimlik Doğrulama | Cookie Authentication (`Microsoft.AspNetCore.Authentication.Cookies`) |
| Şifre Güvenliği | BCrypt.Net-Next (hash'leme) |
| Excel Oluşturma | ClosedXML |
| Veri Saklama | JSON dosyaları (`System.Text.Json`) |
| Görünüm | Razor Views (.cshtml) |
| Stil | Bootstrap (grid/layout) + özel CSS (koyu tema, neon yeşil/pembe vurgular) |

---

## Proje Yapısı

```
BayiUrunKatalogu/
├── Controllers/
│   ├── AccountController.cs      # Giriş / çıkış işlemleri
│   ├── AdminController.cs        # Ürün ekle / düzenle / sil / Excel'e aktar
│   ├── HomeController.cs         # Anasayfa yönlendirme, gizlilik, hata sayfası
│   └── UrunController.cs         # Ürün listeleme, arama, detay
├── Models/
│   ├── Urun.cs
│   ├── Kullanici.cs
│   ├── KullaniciRolu.cs
│   ├── RenkSecenegi.cs
│   └── RenkYardimcisi.cs         # Renk adı → hex kod eşleştirmesi
├── Services/
│   ├── UrunServisi.cs            # JSON okuma/yazma, CRUD iş mantığı
│   └── KullaniciServisi.cs
├── ViewComponents/
│   └── KategoriMenuViewComponent.cs
├── Views/
│   ├── Urun/          (Index, Detay)
│   ├── Admin/         (Ekle, Duzenle)
│   ├── Home/          (Index, Privacy, HataSayfasi)
│   ├── Account/       (Login)
│   └── Shared/        (_Layout, Error)
├── Data/
│   ├── Urunler.json
│   └── Kullanicilar.json
└── wwwroot/
    ├── css/site.css
    └── images/Urunler/           # Ürün görselleri (marka + renk varyantları)
```

---

## Kurulum ve Çalıştırma

**Gereksinimler:** .NET 9 SDK

1. Depoyu klonla:
   ```
   git clone <repo-url>
   cd BayiUrunKatalogu/BayiUrunKatalogu
   ```

2. Bağımlılıkları yükle:
   ```
   dotnet restore
   ```

3. Projeyi çalıştır:
   ```
   dotnet run
   ```

4. Tarayıcıda aç:
   ```
   http://localhost:5090
   ```

---

## Kullanıcı Rolleri

| Rol | Yetkiler |
|---|---|
| **Bayi** | Ürünleri görüntüleme, arama, detay sayfasını inceleme |
| **Admin** | Bayi yetkilerinin tümü + ürün ekleme/düzenleme/silme, Excel'e aktarma |

> Giriş bilgileri `Data/Kullanicilar.json` içinde tanımlıdır. Şifreler BCrypt ile hash'lenerek saklanır.

---

## Veri Modeli

Her ürün (`Urun.cs`) şu alanları içerir:

- `Id`, `UrunAdi`, `UrunKodu`, `Aciklama`, `Kategori`, `Marka`
- `Fiyat`, `StokAdedi`, `GorselYolu`
- `Renkler` — her biri `Ad` ve `GorselYolu` içeren renk seçenekleri listesi
- `Bedenler` — beden etiketleri listesi (giyim/ayakkabı ürünlerinde dolu, diğerlerinde boş)

---

## Ekran Görüntüleri

> _Buraya ürün listesi, ürün detay sayfası ve admin panelinden ekran görüntüleri eklenebilir._

---

## Geliştirme Notları

- Proje, ürünler ve admin özellikleri (ekleme, düzenleme, silme, Excel'e aktarma) adım adım eklenerek geliştirilmiştir.
- Ürün verisi 20+ ürüne (4 kategori: Elektronik, Giyim, Fitness, Ev Yaşam) ulaşacak şekilde genişletilmiştir.
- Kod tabanı, JSON dosyasını doğrudan okuyup yazan bir servis katmanı (`UrunServisi`) üzerinden ilerler; controller'lar iş mantığını doğrudan içermez.

## Olası Geliştirmeler

- JSON yerine gerçek bir veritabanına (SQL Server / SQLite) geçiş
- Ürün görsellerinin admin panelinden dosya yükleme ile eklenmesi (şu an dosya adı elle yazılıyor)
- Sayfalama (pagination) — ürün sayısı arttıkça liste sayfası için
- Sipariş / stok takip sistemi
- Birim testleri
