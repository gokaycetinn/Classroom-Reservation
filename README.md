# Classroom Reservation System

Sınıf rezervasyon sistemi - ASP.NET Core Razor Pages ile geliştirilmiş web uygulaması.

## Özellikler

- **Kullanıcı Yönetimi**: ASP.NET Core Identity ile güvenli kimlik doğrulama
- **Rol Tabanlı Yetkilendirme**: Admin ve Instructor rolleri
- **Sınıf Rezervasyonu**: Tarih ve saat bazlı rezervasyon sistemi
- **Email Bildirimleri**: SMTP tabanlı email servisi
- **Geri Bildirim Sistemi**: Kullanıcı geri bildirimlerinin yönetimi
- **Raporlama**: Admin panelinde detaylı raporlama
- **Loglama**: Serilog ile kapsamlı loglama sistemi

## Teknolojiler

- ASP.NET Core 9.0
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity
- Razor Pages
- Bootstrap 5
- jQuery
- Serilog

## Kurulum

### Gereksinimler

- .NET 9.0 SDK
- SQL Server (LocalDB veya Full Edition)
- Visual Studio 2022 (önerilen)

### Adımlar

1. Projeyi klonlayın:
```bash
git clone https://github.com/gokaycetin/ClassroomReservation.git
cd ClassroomReservation
```

2. `appsettings.json` dosyasını yapılandırın:
```bash
cp appsettings.example.json appsettings.json
```

3. `appsettings.json` dosyasında aşağıdaki ayarları yapın:
   - **ConnectionStrings**: SQL Server bağlantı stringinizi güncelleyin
   - **SmtpSettings**: Email servisi için SMTP ayarlarını yapın

4. Veritabanını oluşturun:
```bash
dotnet ef database update
```

5. Uygulamayı çalıştırın:
```bash
dotnet run
```

## Yapılandırma

### Veritabanı Bağlantısı

`appsettings.json` dosyasında `ConnectionStrings` bölümünü güncelleyin:

```json
{
  "ConnectionStrings": {
    "ClassroomDbConnection": "Server=YOUR_SERVER;Database=YOUR_DATABASE;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### Email Ayarları

SMTP ayarlarınızı yapılandırın:

```json
{
  "SmtpSettings": {
    "Server": "smtp.gmail.com",
    "Port": 587,
    "User": "your-email@gmail.com",
    "Password": "your-app-password"
  }
}
```

**Not**: Gmail kullanıyorsanız, 2FA aktif olmalı ve uygulama şifresi oluşturmalısınız.

## Kullanım

### Varsayılan Kullanıcılar

Sistem ilk çalıştırıldığında varsayılan kullanıcılar oluşturulur:

- **Admin**: `admin@example.com` / `Admin123!`
- **Instructor**: `instructor@example.com` / `Instructor123!`

### Roller

- **Admin**: Tüm sistem yönetimi, kullanıcı yönetimi, raporlama
- **Instructor**: Sınıf rezervasyonu, kendi rezervasyonlarını görüntüleme
- **User**: Temel kullanıcı işlemleri

## Proje Yapısı

```
ClassroomReservation/
├── Data/                   # Entity Framework DbContext
├── Models/                 # Veri modelleri
├── Pages/                  # Razor Pages
│   ├── Admin/             # Admin paneli sayfaları
│   ├── Instructor/        # Instructor paneli sayfaları
│   └── Shared/            # Paylaşılan layout ve bileşenler
├── Services/              # İş mantığı servisleri
├── Migrations/            # Entity Framework migrasyonları
└── wwwroot/              # Statik dosyalar (CSS, JS, images)
```

## Katkıda Bulunma

1. Bu repoyu fork edin
2. Feature branch oluşturun (`git checkout -b feature/amazing-feature`)
3. Değişikliklerinizi commit edin (`git commit -m 'Add amazing feature'`)
4. Branch'inizi push edin (`git push origin feature/amazing-feature`)
5. Pull Request oluşturun

## Lisans

Bu proje MIT lisansı ile lisanslanmıştır. Detaylar için `LICENSE` dosyasına bakın.

## İletişim

Gökay Çetin - gokaycetin10@gmail.com

Proje Linki: [https://github.com/gokaycetin/ClassroomReservation](https://github.com/gokaycetin/ClassroomReservation)

## Güvenlik Notları

- `appsettings.json` dosyası `.gitignore` listesinde bulunmaktadır
- Hassas bilgiler için environment variables kullanın
- Production ortamında güçlü şifreler kullanın
- HTTPS kullanımı zorunludur
