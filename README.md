# Hair Salon Appointment Management System ✂️

Bu proje, **C# Windows Forms** kullanılarak geliştirilmiş, **Nesne Yönelimli Programlama (OOP)** prensiplerini temel alan kapsamlı bir randevu ve işletme yönetim sistemidir. Uygulama; çalışan, müşteri, hizmet ve randevu süreçlerini modüler bir yapıda yönetirken, verileri yerel dosyalarda depolayarak veri sürekliliğini sağlar.

## 🚀 Öne Çıkan Teknik Özellikler

* **OOP Mimari ve Kalıtım:** `Person` ana sınıfından türetilen `Employee` ve `Customer` sınıfları ile kod tekrarı önlenmiş, profesyonel bir sınıf hiyerarşisi kurulmuştur.
* **Dinamik Veri ve Dosya Yönetimi:** Uygulama kapatılsa bile verilerin kaybolmaması için tüm kayıtlar (müşteri, çalışan, hizmet) dosya sisteminde (`.txt`) tutulur ve form yüklendiğinde otomatik olarak çekilir.
* **Gelişmiş Validasyon (Hata Kontrolü):**
    * **Çakışma Önleme:** Aynı çalışanın veya müşterinin seçilen saatte başka bir randevusu varsa sistem otomatik olarak uyarı verir ve mükerrer randevuyu engeller.
    * **Zaman Optimizasyonu:** Geçmiş tarihlere randevu alınması engellenmiştir ve randevular 1'er saatlik aralıklarla (örneğin 17:00, 18:00) standardize edilmiştir.
* **Modüler Hizmet Yönetimi:** `Services` statik sınıfı altında hizmetler ve fiyatları `Dictionary` yapısında tutulur; yeni hizmet ekleme veya silme işlemleri dinamik olarak gerçekleştirilebilir.

## 🛠️ Kullanılan Teknolojiler

* **Dil:** C#
* **Platform:** .NET Framework / Windows Forms
* **Veri Depolama:** File I/O (Dosya İşlemleri)
* **Programlama Yaklaşımı:** Object-Oriented Programming (OOP)

## 📂 Proje Yapısı

* **`Person.cs`:** Temel kimlik bilgilerini ve randevu zamanlarını (`List<DateTime>`) yöneten ana sınıf.
* **`Employee.cs` & `Customer.cs`:** `Person` sınıfından kalıtım alan, çalışan ve müşteri rollerine özgü yetenekleri barındıran sınıflar.
* **`Services.cs`:** İşletme hizmetlerini ve ücretlendirme mantığını yöneten merkezi statik sınıf.
* **Randevu Modülü:** Müşteri, çalışan ve hizmet seçimini entegre eden, veritabanı benzeri dosya okuma/yazma işlemlerini yürüten karar mekanizması.

---
*Bu proje, Sakarya Üniversitesi Nesneye Dayalı Programlama dersi kapsamında, yazılım mimarisi ve veri yönetimi yetkinliklerini pekiştirmek amacıyla geliştirilmiştir.*
