using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuYonetici : MonoBehaviour
{
    [SerializeField] GameObject anaPanel;
    [SerializeField] GameObject ayarlarPaneli;
    [SerializeField] TMP_Text sesYazisi;
    [SerializeField] TMP_Text zorlukYazisi;
    [SerializeField] TMP_Text hizYazisi;
    [SerializeField] AudioSource hoparlor;
    [SerializeField] AudioClip tikSesi;

    void Start()
    {
        anaPanel.SetActive(true);
        ayarlarPaneli.SetActive(false);
        YazilariGuncelle();
    }

    public void Oyna()
    {
        Tikla();
        SceneManager.LoadScene("Oyun");
    }

    public void AyarlariAc()
    {
        Tikla();
        anaPanel.SetActive(false);
        ayarlarPaneli.SetActive(true);
    }

    public void GeriDon()
    {
        Tikla();
        ayarlarPaneli.SetActive(false);
        anaPanel.SetActive(true);
    }

    public void SesDegistir()
    {
        if (PlayerPrefs.GetInt("ses", 1) == 1)
        {
            PlayerPrefs.SetInt("ses", 0);
        }
        else
        {
            PlayerPrefs.SetInt("ses", 1);
        }
        Tikla();
        YazilariGuncelle();
    }

    public void ZorlukSec(int zorluk)
    {
        PlayerPrefs.SetInt("zorluk", zorluk);
        Tikla();
        YazilariGuncelle();
    }

    public void HizSec(int hiz)
    {
        PlayerPrefs.SetInt("sepetHizi", hiz);
        Tikla();
        YazilariGuncelle();
    }

    void YazilariGuncelle()
    {
        if (PlayerPrefs.GetInt("ses", 1) == 1)
        {
            sesYazisi.text = "Ses: Açık";
        }
        else
        {
            sesYazisi.text = "Ses: Kapalı";
        }

        if (PlayerPrefs.GetInt("zorluk", 0) == 0)
        {
            zorlukYazisi.text = "Zorluk: Kolay";
        }
        else
        {
            zorlukYazisi.text = "Zorluk: Zor";
        }

        int hiz = PlayerPrefs.GetInt("sepetHizi", 1);
        if (hiz == 0)
        {
            hizYazisi.text = "Sepet Hızı: Yavaş";
        }
        else if (hiz == 1)
        {
            hizYazisi.text = "Sepet Hızı: Normal";
        }
        else
        {
            hizYazisi.text = "Sepet Hızı: Hızlı";
        }
    }

    void Tikla()
    {
        if (PlayerPrefs.GetInt("ses", 1) == 1)
        {
            hoparlor.PlayOneShot(tikSesi);
        }
    }
}
