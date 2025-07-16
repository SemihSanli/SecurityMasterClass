# 🚀 .NET Core 9.0 - Güvenlik Odaklı Kullanıcı Yönetimi ve Mesajlaşma Sistemi 🔐📩

---

## 📖 Proje Hakkında

Bu proje, **.NET Core 9.0** ile tek katmanda geliştirilmiş, güvenlik ve kullanıcı deneyimi odaklı modern bir web uygulamasıdır.  
Kullanıcı kayıt, giriş, rol yönetimi, mesajlaşma ve yorum sistemi gibi temel özellikleri güçlü güvenlik önlemleriyle destekler.  

---

## 🌟 Öne Çıkan Özellikler

| Özellik                          | Açıklama                                                                                  | Emoji     |
|---------------------------------|-------------------------------------------------------------------------------------------|-----------|
| **2 Adımlı Doğrulama**           | Kullanıcı kayıt olurken mail doğrulaması zorunlu. Doğrulama yapılmadan giriş yapılamaz.    | 📧🔒      |
| **Mesajlaşma Sistemi**            | Kullanıcılar kendilerine gelen mesajları görüntüleyip, detayına gidip cevaplayabilir.    | 💌📬      |
| **Rol Yönetimi**                  | Admin rolü ile kullanıcıya rol atama, rol sayfası erişim kısıtlamaları ve 403 sayfası.    | 👤🔑       |
| **JWT Token ile Kimlik Doğrulama**| HMAC256 algoritması ile şifrelenmiş tokenlar. Token süresi 5 dk, refresh token ile uzatma.| 🔐⏳      |
| **Hata Sayfaları**                | 401, 403, 404 hata sayfalarıyla güvenli ve kullanıcı dostu hata yönetimi.                 | ⚠️📄      |
| **Şifre Sıfırlama**               | "Şifremi unuttum" ile mail adresine reset linki gönderme ve şifre yenileme.               | 🔄🔑      |
| **Güvenlik Mesajları**            | Girişte "kullanıcı adı veya şifre yanlış" gibi genel hata mesajları ile güvenlik sağlama.| 🚫🔍      |
| **ToxicBERT ile Yorum Denetimi** | Yapay zeka destekli toksik yorum tespiti, Helsinki-NLP ile çeviri ve admin onayı.         | 🤖🛡️      |
| **Google ile Giriş**              | Google OAuth ile hızlı ve güvenli sosyal giriş imkanı.                                    | 🟢👥       |

---

## 🏗️ Proje Detayları

### 1. Kullanıcı Kayıt ve Doğrulama

- Kayıt sırasında **MailKit SMTP** kullanılarak kullanıcıya doğrulama maili gönderilir.  
- Kullanıcı mail adresini doğrulamadan **giriş yapamaz**.  
- Doğrulama yapılmadan giriş denemesi yapıldığında uyarı verilir.

---

### 2. Mesajlaşma Sistemi

- Kullanıcılar kendi gelen kutusundaki mesajları görebilir, detayına tıklayabilir ve cevaplayabilir.  
- Gönderilen mesajlar da kullanıcı tarafından listelenebilir.  
- Mesajlaşma UI/UX, kullanıcı deneyimini artıracak şekilde tasarlanmıştır.

---

### 3. Rol Yönetimi ve Erişim Kontrolü

- Admin kullanıcılar **roller oluşturabilir** ve **kullanıcılara rol atayabilir**.  
- Örneğin, sadece **Admin rolü olan kullanıcılar** rol yönetimi sayfalarına erişebilir.  
- Erişim engellenen sayfalarda **403 Forbidden** hata sayfası gösterilir.  
- Kısıtlanan sayfalar:  
  - Rol listesi, rol atama sayfası  
  - Kullanıcı listesi  
  - Yorum yönetimi

---

### 4. JWT ve Token Yönetimi

- Kullanıcı girişinde JWT token üretilir ve HMAC256 algoritması ile şifrelenir.  
- Token süresi **5 dakika** olarak ayarlanmıştır.  
- Kısa süre nedeniyle **refresh token mekanizması** ile kullanıcı yeniden login olmadan token yenileyebilir.  
- Token güvenliği sistemin temelini oluşturur.

---

### 5. Hata Sayfaları

| Hata Kodu | Açıklama                        | Sayfa          | Emoji  |
|-----------|--------------------------------|----------------|--------|
| 401       | Yetkisiz erişim (Unauthorized) | Kullanıcı giriş gerektirir | 🚫     |
| 403       | Yasaklanmış erişim (Forbidden) | Rol kısıtlaması sonucu | 🔒     |
| 404       | Sayfa bulunamadı               | Yanlış URL veya eksik kaynak | ❓     |

---

### 6. Şifre Sıfırlama Akışı

- Kullanıcı "Şifremi unuttum" butonuna tıklar.  
- Kendi mail adresini girer ve sistemden reset linki gönderilir.  
- Maildeki linke tıklanarak şifre yenileme sayfasına ulaşır.  
- Bu sayede kullanıcı hesabını güvenli şekilde kurtarabilir.

---

### 7. Girişte Güvenlik

- Kullanıcı adı veya şifreden biri yanlışsa, **"Kullanıcı adı veya şifre yanlış"** genel hata mesajı verilir.  
- Bu yöntem ile sistemde kayıtlı kullanıcı varlığı hakkında bilgi sızdırılmaz.  
- Bruteforce ve kullanıcı tahmini saldırılarına karşı önlem.

---

### 8. Yapay Zeka Destekli Yorum Filtreleme - ToxicBERT

- Kullanıcı forumda yorum yaparken yorum **ToxicBERT** ile analiz edilir.  
- Toksik skoru **0.5 üzerindeki yorumlar** adminin incelemesine düşer.  
- Admin, yorumları silebilir, toksik olarak işaretleyebilir, onaylayabilir veya pasif hale getirebilir.  
- ToxicBERT İngilizce dilinde yüksek doğrulukta olduğu için:  
  - Kullanıcı yorumu önce **Helsinki-NLP modeli** ile İngilizceye çevrilir.  
  - Toksik analizi yapılır.  
  - Sonuç Türkçeye çevrilip yayınlama kararı verilir.

---

### 9. Google ile Sosyal Giriş

- Kullanıcı "Google ile giriş yap" butonuna tıklayarak Google OAuth sayfasına yönlendirilir.  
- Google ile başarılı giriş sonrası, kullanıcının GoogleKey bilgisi veritabanında saklanır.  
- Böylece sosyal giriş ile hızlı ve güvenli oturum açma sağlanır.

---

## 🛠️ Kullanılan Teknolojiler ve Kütüphaneler

| Teknoloji / Kütüphane | Açıklama                           | Versiyon       | Emoji   |
|----------------------|----------------------------------|----------------|---------|
| .NET Core 9.0        | Ana backend framework             | 9.0            | ⚙️       |
| MailKit SMTP         | Mail gönderme ve doğrulama        | Son sürüm      | 📧       |
| JWT (System.IdentityModel.Tokens.Jwt) | Token yönetimi ve güvenlik     | Son sürüm      | 🔐       |
| Helsinki-NLP          | Makine çevirisi (Türkçe -> İngilizce) | Model API      | 🌐       |
| ToxicBERT             | Yapay zeka toksik yorum analizi   | Model API      | 🤖       |
| Google OAuth          | Sosyal giriş                       | API            | 🟢       |

---

## 🗂️ Proje Mimarisi


Uygulama, aşağıdaki katmanları tek bir yapıda birleştiren **tek katmanlı mimari** ile geliştirilmiştir:

- 🎨 **Kullanıcı Arayüzü (Views, Controllers)**  
  Kullanıcının sistemle etkileşimini sağlayan görsel ve yönlendirici yapılar.

- ⚙️ **İş Mantığı (Kullanıcı Kayıt, Rol Yönetimi, Mesajlaşma)**  
  Uygulamanın temel davranışları ve kuralları burada uygulanır.

- 💾 **Veri Katmanı (Entity Framework veya ORM)**  
  Veritabanı işlemleri için Entity Framework veya ORM araçları kullanılır.

- 🔐 **Güvenlik Katmanı (JWT, HMAC256, 2FA, Mail Doğrulama)**  
  Kimlik doğrulama, şifreleme, token yönetimi ve mail doğrulama işlemleri bu alanda yapılır.

- 🌐 **Üçüncü Parti Entegrasyonlar**  
  Aşağıdaki hizmetler dış sistemlerle entegre çalışır:
  - **Google OAuth**: Sosyal giriş
  - **MailKit**: SMTP mail gönderimi
  - **Helsinki-NLP**: Yorumları İngilizce'ye çevirme
  - **ToxicBERT**: Toksik yorum analiz sistemi
 

<img width="1894" height="896" alt="1" src="https://github.com/user-attachments/assets/a4915758-bfa0-4598-998f-a5668fe937ce" />


<img width="1897" height="902" alt="2" src="https://github.com/user-attachments/assets/1501f5d9-1b66-45a7-99cc-4a2414d5e865" />


<img width="1912" height="901" alt="3" src="https://github.com/user-attachments/assets/b6c745ff-7bce-4595-b1ad-9bc67781b80d" />


![4](https://github.com/user-attachments/assets/66bb1df1-9c29-4769-a71a-cba475e049d1)
<img width="1909" height="529" alt="5" src="https://github.com/user-attachments/assets/e26c4a3b-87c3-4b04-9815-5895c9fb169d" />


![6](https://github.com/user-attachments/assets/9943eb43-dc0c-4d0b-8e94-efbc469a3034)

<img width="1914" height="859" alt="7 (2)" src="https://github.com/user-attachments/assets/a6fd1102-7f96-42f2-9743-330240ac163b" />

<img width="1900" height="895" alt="7" src="https://github.com/user-attachments/assets/11ebc9c8-b7df-47f0-8fc8-51c4c89b7ca2" />

<img width="1897" height="904" alt="8" src="https://github.com/user-attachments/assets/4e363543-ef74-401e-a310-383ecc325430" />



<img width="1895" height="900" alt="9" src="https://github.com/user-attachments/assets/a3284105-0b07-455e-84ae-c65212e82f9c" />


<img width="1888" height="739" alt="10" src="https://github.com/user-attachments/assets/3a5c91ca-a005-4908-973f-175a84b67918" />


<img width="1887" height="775" alt="11" src="https://github.com/user-attachments/assets/bdd1bc1a-5f4a-4af9-98d9-7965efd6abf4" />


<img width="1889" height="726" alt="12" src="https://github.com/user-attachments/assets/be64a338-fd84-4d36-8723-d5cd61d53e5a" />


<img width="1868" height="416" alt="13" src="https://github.com/user-attachments/assets/a88b24a8-cb6b-43a3-97a4-02d4e75ca2b2" />

<img width="1885" height="553" alt="14" src="https://github.com/user-attachments/assets/1c1eebc9-7f35-447d-bf78-8da164da9f0b" />
<img width="1898" height="506" alt="16" src="https://github.com/user-attachments/assets/213e6f1f-58b7-4e68-b3ff-2e8621654b3b" />
<img width="1139" height="659" alt="17" src="https://github.com/user-attachments/assets/dc5b86b5-b32b-45a9-932a-1b6409706aa0" />
<img width="1346" height="918" alt="18" src="https://github.com/user-attachments/assets/c88f0e3a-c902-49f5-a047-624529f5c01e" />
<img width="1555" height="619" alt="19" src="https://github.com/user-attachments/assets/f4dcdb17-df2c-4224-a959-fc39b945a472" />
