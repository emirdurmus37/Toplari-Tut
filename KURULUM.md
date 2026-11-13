# TOPLARI TUT

**Kural:** Yukarıdan rastgele yerlerden toplar yağar. Alttaki sepeti parmağınla sağa-sola sürükleyip topları tut. Her tutulan top 1 puan, her kaçan top 1 can götürür.
**Kazanma / kaybetme:** `if (puan >= hedefPuan)` → kazandın (10 puan). `if (can <= 0)` → kaybettin (3 can).
**Kontrol:** Parmakla ya da fareyle basılı tutup sürükleme; sepet sadece sağa-sola gider.

## Ekranlar
1. **Splash** – oyunun adı, 2 sn sonra ya da dokununca menü.
2. **Menü** – Oyna / Ayarlar (ses aç-kapat, zorluk Kolay/Zor, sepet hızı Yavaş/Normal/Hızlı).
3. **Oyun** – toplar, sepet, puan, can, sonuç paneli (Tekrar Oyna / Menü).

## Unity'de kurulum
1. **Window → TextMeshPro → Import TMP Essential Resources**.
2. Üst menü **Toplari Tut → Her Seyi Kur**.
3. Game penceresi **1080x1920 Portrait**, Splash sahnesinde **Play**.

## Dosyalar
| Klasör | Ne var |
|---|---|
| Assets/Scripts | OyunYonetici (oyunun beyni), Sepet, Top, Splash, MenuYonetici |
| Assets/DersBetikleri | Merhaba, Hareket, Sekme, TopUretici (ders deneyleri) |
| Assets/Editor | Sahneleri kuran araç (oyunun parçası değil, Claude ile yazıldı) |
