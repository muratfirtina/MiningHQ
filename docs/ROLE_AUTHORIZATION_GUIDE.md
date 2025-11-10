# Role-Based Authorization - Kullanım Rehberi

## 📋 İki Tip Rol Vardır

### 🔒 1. Sistem Rolleri (Constants)
**Tanım**: `Domain.Constants.Roles` dosyasında tanımlanır
**Amaç**: Kod tarafında kritik kontroller için kullanılır
**Özellikleri**:
- Uygulama mantığının bir parçasıdır
- Compile-time'da bilinir
- Değiştirilmesi deployment gerektirir
- Authorization attribute'larında kullanılır

**Mevcut Sistem Rolleri**:
```csharp
- Admin         // Tam yetki
- Moderator     // Orta seviye yetki
- HRAssistant   // İK işlemleri
```

**Kullanım Örnekleri**:
```csharp
// Authorization kontrolü
public string[] Roles => new[] { Domain.Constants.Roles.Admin };

// Kod içinde kontrol
if (user.HasRole(Domain.Constants.Roles.Admin))
{
    // Kritik işlem
}
```

### 🎭 2. İş Rolleri (Dynamic - Database)
**Tanım**: Sadece veritabanında tutulur
**Amaç**: Runtime'da oluşturulan iş rolleri
**Özellikleri**:
- Dinamik olarak oluşturulabilir
- Kod değişikliği gerektirmez
- Yönetici tarafından tanımlanır
- Authorization'da claim olarak kontrol edilir

**Örnek İş Rolleri**:
```
- Manager
- Supervisor
- DataEntry
- Accountant
- WarehouseManager
```

---

## ❓ Yeni Rol Ekleme Kararı

### Ne Zaman Domain.Constants.Roles'e Eklerim?

✅ **Constants'a EKLE** eğer:
- Rol uygulama mantığında kullanılacaksa
- Kod tarafında özel davranış gerekiyorsa
- Kritik sistem işlemlerini kontrol ediyorsa
- Hardcode kontrol yapmanız gerekiyorsa

**Örnek**: Sadece Admin'in yapabileceği sistem ayarları

```csharp
// Constants'a ekle
public const string SystemAdmin = "SystemAdmin";

// Kod içinde kullan
if (user.HasRole(Roles.SystemAdmin))
{
    // Kritik sistem ayarı
}
```

### Ne Zaman Sadece Database'e Eklerim?

✅ **Sadece DATABASE'e EKLE** eğer:
- İş süreçlerine özel roller ise
- Dinamik olarak yönetilmesi gerekiyorsa
- Kod tarafında özel davranış gerekmiyorsa
- Sadece yetkilendirme için kullanılacaksa

**Örnek**: Departman bazlı roller

```bash
# API ile oluştur, Constants'a ekleme!
POST /api/roles
{
  "name": "DepartmentManager",
  "description": "Departman yöneticisi"
}
```

---

## 🎯 Authorization Nasıl Çalışır?

### Token'daki Claim'ler

Kullanıcı login olduğunda token'a şunlar eklenir:

1. **Rol'den gelen claim'ler** (RoleOperationClaim)
2. **Doğrudan atanan claim'ler** (UserOperationClaim)

```json
{
  "claims": [
    "Admin",                    // Sistem rolü (Constants'tan)
    "Manager",                  // İş rolü (Database'den)
    "Machines.Read",           // Rolden gelen claim
    "Machines.Write",          // Rolden gelen claim
    "SpecialPermission.X"      // Doğrudan atanan claim
  ]
}
```

### Authorization Kontrolü

```csharp
// Örnek 1: Sadece Admin
public string[] Roles => new[] { Domain.Constants.Roles.Admin };

// Örnek 2: Admin VEYA spesifik claim
public string[] Roles => new[] {
    Domain.Constants.Roles.Admin,
    "Machines.Create"
};

// Örnek 3: Herhangi bir rol claim'i (önerilmez)
public string[] Roles => new[] {
    Domain.Constants.Roles.Admin,
    Domain.Constants.Roles.Moderator,
    "Machines.Create"
};
```

---

## 💡 Best Practices

### ✅ DO (Yapın)

1. **Sistem rolleri için Constants kullanın**
   ```csharp
   public string[] Roles => new[] { Domain.Constants.Roles.Admin };
   ```

2. **Granular permission'lar ekleyin**
   ```csharp
   public string[] Roles => new[] {
       Domain.Constants.Roles.Admin,
       MaintenancesOperationClaims.Create
   };
   ```

3. **İş rolleri için Database kullanın**
   ```bash
   POST /api/roles { "name": "WarehouseManager" }
   ```

### ❌ DON'T (Yapmayın)

1. **Her yeni rolü Constants'a eklemeyin**
   ```csharp
   // ❌ YANLIŞ - İş rolü Constants'ta
   public const string WarehouseManager = "WarehouseManager";
   ```

2. **Rol ismi ile hardcode kontrol yapmayın**
   ```csharp
   // ❌ YANLIŞ
   if (user.RoleName == "Manager") { }

   // ✅ DOĞRU - Claim kontrolü
   if (user.HasClaim("Managers.Action")) { }
   ```

3. **LoginCommand'da filtreleme yapmayın**
   ```csharp
   // ❌ YANLIŞ - Sadece sistem rollerini gösterir
   .Where(name => DomainRoles.All.Contains(name))

   // ✅ DOĞRU - Tüm rolleri göster
   var userRoles = await _userRoleRepository
       .Query()
       .Where(ur => ur.UserId == user.Id)
       .Select(ur => ur.Role.Name)
       .ToListAsync();
   ```

---

## 📊 Karar Ağacı

```
Yeni Rol Ekleyeceksiniz?
│
├─ Kod içinde özel davranış gerekiyor mu?
│  ├─ Evet → Domain.Constants.Roles'e ekle
│  └─ Hayır → ↓
│
├─ Sadece yetkilendirme için mi?
│  ├─ Evet → Sadece Database'e ekle (API ile)
│  └─ Hayır → ↓
│
├─ Sistem kritik işlem mi?
│  ├─ Evet → Domain.Constants.Roles'e ekle
│  └─ Hayır → Sadece Database'e ekle
```

---

## 🔄 Örnek Senaryo

### Senaryo: "FinanceManager" rolü eklenecek

**Analiz**:
- İş sürecine özel ✅
- Kod tarafında özel davranış yok ✅
- Sadece yetkilendirme için ✅

**Karar**: Sadece Database'e ekle

```bash
# 1. Rol oluştur
POST /api/roles
{
  "name": "FinanceManager",
  "description": "Finans yöneticisi"
}

# 2. Role claim'ler ata
POST /api/role-operation-claims/assign
{
  "roleId": 4,
  "operationClaimId": 250  # "Finance.Read"
}

# 3. Kullanıcıya rol ata
POST /api/user-roles/assign
{
  "userId": "user-guid",
  "roleId": 4
}
```

**Domain.Constants.Roles'e EKLEME!** ❌

---

## 📝 Özet

- **Domain.Constants.Roles** = Sadece kritik sistem rolleri
- **Database Roles** = İş süreçlerine özel dinamik roller
- **Yeni rol** → Önce "Kod içinde özel davranış gerekiyor mu?" diye sor
- **Evet** → Constants'a ekle
- **Hayır** → API ile database'e ekle
