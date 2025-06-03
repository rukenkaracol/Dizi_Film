

---

## Dizi-Film Takip Sistemi

### Proje Linki



---

### Proje Hakkında

Bu proje, kullanıcıların izledikleri dizi ve filmleri takip edebileceği bir masaüstü uygulamasıdır. Uygulama C# ve SQL Server kullanılarak geliştirilmiştir.

---

### Kullanılan Teknolojiler

* C# (.NET Windows Forms)
* SQL Server
* ADO.NET
* GitHub

---

### Giriş / Kayıt Sistemi

* Kullanıcılar e-posta ve şifre ile giriş yapar.
* Üye olmayanlar "Kayıt Ol" butonuyla hesap oluşturabilir.
* Admin kullanıcılar içerik yönetimi yetkisine sahiptir.


---

### Kullanıcı Paneli

* İçerikler listelenebilir, filtrelenebilir ve puanlanabilir.
* Kullanıcılar sadece kendi izleme geçmişini görebilir.
* İzleme durumu: "İzliyor", "Tamamlandı", "Bırakıldı".



---

### Admin Paneli

* Yeni içerik eklenebilir, güncellenebilir veya silinebilir.
* Tüm içerikler ContentID’ye göre görüntülenebilir.



---

### Veritabanı Tasarımı

Üç temel tablo kullanılmıştır:

* `Users`
* `Contents`
* `WatchRecords`

Bu tablolar arasında ilişkiler bulunmaktadır.


