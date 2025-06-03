

---

## Dizi-Film Takip Sistemi

### Proje Linki

[GitHub Repository](https://github.com/rukenkaracol/DiziTakip)

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

![frmGiris](attachment\:file-Kpphp6NVAjcs9gNDjPjRbB)

---

### Kullanıcı Paneli

* İçerikler listelenebilir, filtrelenebilir ve puanlanabilir.
* Kullanıcılar sadece kendi izleme geçmişini görebilir.
* İzleme durumu: "İzliyor", "Tamamlandı", "Bırakıldı".

![frmKullanici](attachment\:file-DTFygs7CvbyxEUuiPQtcAH)

---

### Admin Paneli

* Yeni içerik eklenebilir, güncellenebilir veya silinebilir.
* Tüm içerikler ContentID’ye göre görüntülenebilir.

![frmAdmin](attachment\:file-UKiG2Qo5jQbmbTT9Sy45yx)

---

### Veritabanı Tasarımı

Üç temel tablo kullanılmıştır:

* `Users`
* `Contents`
* `WatchRecords`

Bu tablolar arasında ilişkiler bulunmaktadır.
![Veritabanı Şeması](attachment\:file-KdyJyZXeNy98144MPf63co)

---

### İş Kuralları

1. Kayıtsız kullanıcılar sadece içerikleri görüntüleyebilir.
2. Kayıtlı kullanıcılar içerik ekleyip puan verebilir.
3. İçerikler bir kez puanlanabilir, puan güncellenebilir.
4. Kullanıcılar sadece kendi geçmişini görebilir.
5. Silinen içerikler tüm geçmişten kaldırılır.
