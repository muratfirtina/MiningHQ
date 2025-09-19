# MiningHQ Backend - Mining Operations Management System

## 📋 Proje Hakkında

MiningHQ Backend, maden işletmeleri için tasarlanmış kapsamlı bir yönetim sistemi API'sidir. Bu sistem, personel yönetimi, makine takibi, günlük iş verileri, yakıt tüketimi, bakım yönetimi ve izin takibi gibi maden işletmelerinin temel ihtiyaçlarını karşılayan modern bir Clean Architecture yaklaşımıyla geliştirilmiştir.

## 🏗️ Mimari Yapısı

Bu proje, Clean Architecture prensiplerine göre tasarlanmış ve aşağıdaki katmanlardan oluşmaktadır:

### 🔧 Core Packages (Ortak Kütüphaneler)
- **Core.Application**: CQRS, MediatR, AutoMapper, pipelines
- **Core.Persistence**: Entity Framework, Repository Pattern, Dynamic Query
- **Core.Security**: JWT, Authentication, Authorization, 2FA
- **Core.CrossCuttingConcerns**: Exception Handling, Logging, Validation
- **Core.Mailing**: Email gönderimi (MailKit entegrasyonu)
- **Core.ElasticSearch**: Arama ve veri indeksleme
- **Core.Test**: Test altyapısı ve mock helpers
- **Core.WebAPI**: Swagger entegrasyonu ve API ortak yapılar

### 🎯 MiningHQ Specific Layers
- **Domain**: Entity'ler, Enum'lar, Domain Logic
- **Application**: Use Cases, CQRS Commands/Queries, Business Rules
- **Persistence**: Entity Framework DbContext, Repository implementasyonları
- **Infrastructure**: External servisler, Cloud storage entegrasyonları
- **WebAPI**: Controllers, API endpoints, middleware'ler

## 🗃️ Veritabanı Yapısı

### Ana Entity'ler

#### 👥 İnsan Kaynakları
- **Employee**: Çalışan bilgileri (ad, soyad, doğum tarihi, işe giriş/çıkış tarihleri, kan grubu, acil durum iletişim)
- **Department**: Departman yönetimi
- **Job**: Pozisyon/görev tanımları
- **EmployeePhoto**: Çalışan fotoğrafları
- **EmployeeFile**: Çalışan dosyaları
- **Timekeeping**: Mesai takibi
- **Overtime**: Fazla mesai kayıtları

#### 🏭 İzin Yönetimi
- **LeaveType**: İzin türleri (yıllık, hastalık, mazeret vb.)
- **EntitledLeave**: Çalışanların hak ettiği izinler
- **EmployeeLeaveUsage**: Kullanılan izin kayıtları

#### 🚛 Makine ve Ekipman Yönetimi
- **Machine**: Makine kayıtları
- **Brand**: Marka bilgileri (Caterpillar, Volvo, Liebherr vb.)
- **Model**: Makine modelleri
- **MachineType**: Makine türleri (ekskavatör, damperli kamyon vb.)
- **Maintenance**: Bakım kayıtları
- **MaintenanceType**: Bakım türleri
- **MaintenanceFile**: Bakım dosyaları

#### 📊 Operasyonel Veriler
- **DailyWorkData**: Günlük iş verileri (çalışma saatleri, üretim miktarları)
- **DailyFuelConsumptionData**: Günlük yakıt tüketimi verileri
- **Quarry**: Ocak/maden sahası bilgileri

#### 📎 Dosya Yönetimi
- **File**: Dosya yönetimi (çoklu cloud storage desteği)

### 🔐 Güvenlik Yapısı
- **User**: Kullanıcı hesapları (Core.Security'den extend)
- **OperationClaim**: Yetki tanımları
- **UserOperationClaim**: Kullanıcı-yetki ilişkileri
- **RefreshToken**: JWT refresh token'ları
- **EmailAuthenticator & OtpAuthenticator**: 2FA desteği

## 🚀 Kullanılan Teknolojiler

### Backend Framework & ORM
- **.NET 7.0**: Modern C# özellikleri
- **Entity Framework Core**: Code-First yaklaşımı
- **PostgreSQL**: Ana veritabanı
- **AutoMapper**: Object-Object mapping
- **MediatR**: CQRS pattern implementasyonu

### Authentication & Security
- **JWT Bearer Tokens**: API authentication
- **2FA Support**: Email ve OTP tabanlı iki faktörlü doğrulama
- **BCrypt**: Password hashing
- **Role-based Authorization**: Rol tabanlı yetkilendirme

### Cloud Storage & File Management
- **Multiple Storage Providers**:
  - Local Storage
  - Azure Blob Storage
  - AWS S3
  - Google Cloud Storage
  - Cloudinary (image processing)

### Logging & Monitoring
- **Serilog**: Yapısal logging
- **Multiple Log Destinations**:
  - File logging
  - PostgreSQL
  - ElasticSearch
  - MongoDB
  - Graylog
  - RabbitMQ
  - MS SQL Server

### Caching & Search
- **Redis**: Distributed caching
- **ElasticSearch**: Full-text search ve analytics

### Email & Communications
- **MailKit**: SMTP email gönderimi
- **Email Templates**: HTML email templates

## 📡 API Endpoints

### 🔐 Authentication
```
POST /api/auth/register - Kullanıcı kaydı
POST /api/auth/login - Giriş
POST /api/auth/refresh-token - Token yenileme
POST /api/auth/revoke-token - Token iptal
POST /api/auth/enable-email-authenticator - Email 2FA aktifleştirme
POST /api/auth/verify-email-authenticator - Email 2FA doğrulama
POST /api/auth/enable-otp-authenticator - OTP 2FA aktifleştirme
POST /api/auth/verify-otp-authenticator - OTP 2FA doğrulama
```

### 👥 Employee Management
```
GET /api/employees - Çalışan listesi
GET /api/employees/{id} - Çalışan detayı
POST /api/employees - Yeni çalışan
PUT /api/employees - Çalışan güncelleme
DELETE /api/employees/{id} - Çalışan silme
```

### 🚛 Machine Management
```
GET /api/machines - Makine listesi
GET /api/machines/{id} - Makine detayı
POST /api/machines - Yeni makine
PUT /api/machines - Makine güncelleme
DELETE /api/machines/{id} - Makine silme
```

### 📊 Data Management
```
GET /api/dailyworkdatas - Günlük iş verileri
POST /api/dailyworkdatas - Yeni iş verisi
GET /api/dailyfuelconsumptiondatas - Yakıt tüketimi verileri
POST /api/dailyfuelconsumptiondatas - Yeni yakıt verisi
```

### 🔧 Maintenance
```
GET /api/maintenances - Bakım kayıtları
POST /api/maintenances - Yeni bakım kaydı
GET /api/maintenance-types - Bakım türleri
```

### 🏢 Organization
```
GET /api/departments - Departmanlar
GET /api/jobs - Pozisyonlar
GET /api/quarries - Ocaklar/Sahalar
GET /api/brands - Markalar
GET /api/models - Modeller
```

## ⚙️ Kurulum ve Çalıştırma

### Gereksinimler
- .NET 7.0 SDK
- PostgreSQL 13+
- Redis (opsiyonel - caching için)
- ElasticSearch (opsiyonel - search için)

### Kurulum Adımları

1. **Repository'yi klonlayın**
```bash
git clone [repository-url]
cd MiningHQ
```

2. **Bağımlılıkları yükleyin**
```bash
dotnet restore
```

3. **Veritabanı ayarlarını yapılandırın**
`appsettings.json` dosyasında PostgreSQL connection string'ini ayarlayın:
```json
{
  "ConnectionStrings": {
    "PostgreSQL": "User ID=postgres;Password=yourpassword;Host=localhost;Port=5432;Database=MiningHQDbContext;"
  }
}
```

4. **Migration'ları uygulayın**
```bash
cd src/miningHQ/Persistence
dotnet ef database update --startup-project ../WebAPI
```

5. **Uygulamayı çalıştırın**
```bash
cd src/miningHQ/WebAPI
dotnet run
```

API, `http://localhost:5278` adresinde çalışacaktır.
Swagger UI: `http://localhost:5278/swagger`

## 🔧 Yapılandırma

### JWT Configuration
```json
{
  "TokenOptions": {
    "AccessTokenExpiration": 10,
    "Audience": "miningHQ@kodlama.io",
    "Issuer": "nArchitecture@kodlama.io",
    "RefreshTokenTTL": 2,
    "SecurityKey": "strongandsecretkeystrongandsecretkey"
  }
}
```

### Storage Configuration
```json
{
  "StorageUrls": {
    "StorageProvider": "LocalStorage",
    "AzureStorageUrl": "https://mininghq.blob.core.windows.net/",
    "GoogleStorageUrl": "https://storage.cloud.google.com/mininghq",
    "AWSStorageUrl": "https://mininghq.s3.eu-north-1.amazonaws.com/",
    "LocalStorageUrl": "http://localhost:5278/"
  }
}
```

## 🏗️ Geliştirici Notları

### CQRS Pattern
Projede CQRS pattern kullanılmaktadır:
- **Commands**: Veri değiştiren işlemler (Create, Update, Delete)
- **Queries**: Veri okuma işlemleri (GetById, GetList)

### Business Rules
Her feature için business rules sınıfı mevcuttur:
```csharp
public class EmployeeBusinessRules : BaseBusinessRules
{
    public Task EmployeeShouldExistWhenSelected(Employee? employee)
    {
        if (employee == null)
            throw new BusinessException(EmployeesBusinessMessages.EmployeeNotExists);
        return Task.CompletedTask;
    }
}
```

### Validation
FluentValidation kullanılarak input validation:
```csharp
public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(c => c.FirstName).NotEmpty();
        RuleFor(c => c.LastName).NotEmpty();
    }
}
```

### Caching
Redis ile automatic caching:
```csharp
public class GetListEmployeeQuery : IRequest<GetListResponse<GetListEmployeeListItemDto>>, 
    ICachableRequest
{
    public string CacheKey => $"GetListEmployee({PageRequest.PageIndex},{PageRequest.PageSize})";
    public TimeSpan? SlidingExpiration { get; set; }
}
```

## 🧪 Testing

Test projesi mevcuttur:
```bash
cd tests/Application.Tests
dotnet test
```

## 📈 Performance Features

- **Pagination**: Büyük veri setleri için sayfalama
- **Dynamic Filtering**: Dinamik filtreleme ve sıralama
- **Caching**: Redis ile otomatik cache yönetimi
- **Lazy Loading**: Entity Framework lazy loading
- **Async/Await**: Asenkron programlama

## 🔒 Güvenlik Özellikleri

- JWT based authentication
- Role-based authorization
- 2FA support (Email & OTP)
- Password hashing with BCrypt
- CORS policy configuration
- Request validation pipelines
- Business rule validation

## 📝 Logging

Comprehensive logging with Serilog:
- Structured logging
- Multiple sinks (File, Database, ElasticSearch)
- Request/Response logging
- Error tracking
- Performance monitoring

## 🌐 API Versioning & Documentation

- Swagger/OpenAPI documentation
- Bearer token authentication in Swagger
- Detailed API documentation
- Request/Response examples

Bu backend API, modern maden işletmelerinin tüm operasyonel ihtiyaçlarını karşılayacak şekilde tasarlanmış olup, yüksek performans, güvenlik ve ölçeklenebilirlik özelliklerine sahiptir.
