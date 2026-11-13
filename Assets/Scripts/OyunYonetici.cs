using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class OyunYonetici : MonoBehaviour
{
    [Header("Oyun ayarları")]
    [SerializeField] Top topPrefab;
    [SerializeField] Sepet sepet;
    [SerializeField] int hedefPuan = 10;
    [SerializeField] int baslangicCan = 3;

    [Header("Yazılar")]
    [SerializeField] TMP_Text puanYazisi;
    [SerializeField] TMP_Text canYazisi;
    [SerializeField] GameObject sonucPaneli;
    [SerializeField] TMP_Text sonucYazisi;

    [Header("Sesler")]
    [SerializeField] AudioSource hoparlor;
    [SerializeField] AudioClip tutmaSesi;
    [SerializeField] AudioClip kacirmaSesi;
    [SerializeField] AudioClip kazanmaSesi;
    [SerializeField] AudioClip kaybetmeSesi;

    int puan = 0;
    int can;
    float topHizi;
    float aralik;
    float sayac = 0f;
    float yanSinir;
    bool oyunBitti = false;

    void Start()
    {
        sonucPaneli.SetActive(false);
        can = baslangicCan;

        if (PlayerPrefs.GetInt("zorluk", 0) == 0)
        {
            topHizi = 3f;
            aralik = 1.5f;
        }
        else
        {
            topHizi = 5f;
            aralik = 1f;
        }

        yanSinir = Camera.main.orthographicSize * Camera.main.aspect - 0.5f;
        YazilariGuncelle();
    }

    void Update()
    {
        if (oyunBitti) return;

        sayac = sayac + Time.deltaTime;
        if (sayac >= aralik)
        {
            sayac = 0f;
            TopYagdir();
        }
    }

    void TopYagdir()
    {
        float x = Random.Range(-yanSinir, yanSinir);
        float y = Camera.main.orthographicSize + 1f;

        Top yeniTop = Instantiate(topPrefab, new Vector3(x, y, 0f), Quaternion.identity);
        yeniTop.hiz = topHizi;
        yeniTop.yonetici = this;
        yeniTop.GetComponent<SpriteRenderer>().color = Random.ColorHSV(0f, 1f, 0.6f, 0.9f, 0.9f, 1f);
    }

    public void TopTutuldu()
    {
        if (oyunBitti) return;

        puan = puan + 1;
        SesCal(tutmaSesi);
        YazilariGuncelle();

        if (puan >= hedefPuan)
        {
            Bitir(true);
        }
    }

    public void TopKacti()
    {
        if (oyunBitti) return;

        can = can - 1;
        SesCal(kacirmaSesi);
        YazilariGuncelle();

        if (can <= 0)
        {
            Bitir(false);
        }
    }

    void YazilariGuncelle()
    {
        puanYazisi.text = "Puan: " + puan + " / " + hedefPuan;
        canYazisi.text = "Can: " + can;
    }

    void Bitir(bool kazandi)
    {
        oyunBitti = true;
        sepet.hareketEdebilir = false;
        sonucPaneli.SetActive(true);

        if (kazandi)
        {
            sonucYazisi.text = "KAZANDIN!\nPuan: " + puan;
            SesCal(kazanmaSesi);
        }
        else
        {
            sonucYazisi.text = "KAYBETTİN!\nPuan: " + puan;
            SesCal(kaybetmeSesi);
        }
        Debug.Log(sonucYazisi.text);
    }

    public void TekrarOyna()
    {
        SceneManager.LoadScene("Oyun");
    }

    public void MenuyeDon()
    {
        SceneManager.LoadScene("Menu");
    }

    void SesCal(AudioClip klip)
    {
        if (PlayerPrefs.GetInt("ses", 1) == 1)
        {
            hoparlor.PlayOneShot(klip);
        }
    }
}
