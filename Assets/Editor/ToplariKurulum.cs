using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.Events;
using UnityEditor.SceneManagement;
using TMPro;

public static class ToplariKurulum
{
    const string SAHNE = "Assets/Scenes/";
    static readonly Color ARKA = new Color(0.07f, 0.18f, 0.2f);
    static readonly Vector2 ORTA = new Vector2(0.5f, 0.5f);
    static Sprite kare, daire, sepetSprite, butonSprite;

    [MenuItem("Toplari Tut/Her Seyi Kur")]
    static void HerSeyiKur()
    {
        if (Resources.Load<TMP_Settings>("TMP Settings") == null)
        {
            EditorUtility.DisplayDialog("Önce TextMeshPro",
                "Önce Window > TextMeshPro > Import TMP Essential Resources ile içe aktar, sonra bu menüyü tekrar çalıştır.", "Tamam");
            return;
        }
        if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) return;

        System.IO.Directory.CreateDirectory("Assets/Scenes");
        System.IO.Directory.CreateDirectory("Assets/Prefabs");
        AssetDatabase.Refresh();

        kare = SpriteHazirla("Assets/Sprites/Kare.png", 64);
        daire = SpriteHazirla("Assets/Sprites/Daire.png", 256);
        sepetSprite = SpriteHazirla("Assets/Sprites/Sepet.png", 100);
        SpriteHazirla("Assets/Sprites/Buton.png", 100);
        var butonImp = (TextureImporter)AssetImporter.GetAtPath("Assets/Sprites/Buton.png");
        butonImp.spriteBorder = new Vector4(48, 48, 48, 48);
        butonImp.SaveAndReimport();
        butonSprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/Buton.png");

        SplashKur();
        MenuKur();
        OyunKur();
        DeneyMerhabaKur();
        DeneyHareketKur();
        DeneySekmeKur();

        EditorBuildSettings.scenes = new[]
        {
            new EditorBuildSettingsScene(SAHNE + "Splash.unity", true),
            new EditorBuildSettingsScene(SAHNE + "Menu.unity", true),
            new EditorBuildSettingsScene(SAHNE + "Oyun.unity", true),
        };
        PlayerSettings.defaultInterfaceOrientation = UIOrientation.Portrait;
        PlayerSettings.productName = "Toplari Tut";

        AssetDatabase.SaveAssets();
        EditorSceneManager.OpenScene(SAHNE + "Splash.unity");
        EditorUtility.DisplayDialog("Hazır!",
            "Sahneler kuruldu. Splash sahnesi açık, Play'e bas.\nGame penceresinde çözünürlüğü 1080x1920 Portrait yap.", "Tamam");
    }

    static void SplashKur()
    {
        var s = YeniSahne(5f, ARKA);
        var c = CanvasKur();
        Color[] renkler = { new Color(1f, 0.45f, 0.3f), new Color(1f, 0.85f, 0.25f), new Color(0.4f, 0.85f, 0.45f) };
        for (int i = 0; i < 3; i++)
            Resim("Top" + i, c, daire, renkler[i], ORTA, new Vector2(-220 + i * 220, 330 - (i % 2) * 120), new Vector2(140, 140));
        Resim("Sepet", c, sepetSprite, Color.white, ORTA, new Vector2(0, -60), new Vector2(520, 260));
        Yazi("Baslik", c, "TOPLARI\nTUT", 130, ORTA, new Vector2(0, 650), new Vector2(1000, 350), Color.white);
        Yazi("AltYazi", c, "dokun ve başla", 55, ORTA, new Vector2(0, -450), new Vector2(900, 100), new Color(1f, 1f, 1f, 0.8f));
        new GameObject("Splash").AddComponent<Splash>();
        EditorSceneManager.SaveScene(s, SAHNE + "Splash.unity");
    }

    static void MenuKur()
    {
        var s = YeniSahne(5f, ARKA);
        var c = CanvasKur();

        var ana = Panel("AnaPanel", c, new Color(0, 0, 0, 0));
        Yazi("Baslik", ana.transform, "TOPLARI\nTUT", 120, ORTA, new Vector2(0, 600), new Vector2(1000, 330), Color.white);
        var oyna = Buton("Oyna", ana.transform, "Oyna", new Vector2(0, 0));
        var ayarlar = Buton("Ayarlar", ana.transform, "Ayarlar", new Vector2(0, -200));

        var ayarP = Panel("AyarlarPaneli", c, new Color(0, 0, 0, 0.55f));
        Yazi("Baslik", ayarP.transform, "AYARLAR", 110, ORTA, new Vector2(0, 750), new Vector2(1000, 180), Color.white);

        var sesYazi = Yazi("SesYazisi", ayarP.transform, "Ses: Açık", 65, ORTA, new Vector2(0, 560), new Vector2(1000, 110), Color.white);
        var sesBtn = Buton("SesButonu", ayarP.transform, "Aç / Kapat", new Vector2(0, 430), new Vector2(500, 130));

        var zorYazi = Yazi("ZorlukYazisi", ayarP.transform, "Zorluk: Kolay", 65, ORTA, new Vector2(0, 230), new Vector2(1000, 110), Color.white);
        var kolay = Buton("Kolay", ayarP.transform, "Kolay", new Vector2(-180, 100), new Vector2(320, 130));
        var zor = Buton("Zor", ayarP.transform, "Zor", new Vector2(180, 100), new Vector2(320, 130));

        var hizYazi = Yazi("HizYazisi", ayarP.transform, "Sepet Hızı: Normal", 65, ORTA, new Vector2(0, -100), new Vector2(1000, 110), Color.white);
        var yavas = Buton("Yavas", ayarP.transform, "Yavaş", new Vector2(-330, -230), new Vector2(300, 130));
        var normal = Buton("Normal", ayarP.transform, "Normal", new Vector2(0, -230), new Vector2(300, 130));
        var hizli = Buton("Hizli", ayarP.transform, "Hızlı", new Vector2(330, -230), new Vector2(300, 130));

        var geri = Buton("Geri", ayarP.transform, "Geri", new Vector2(0, -550));

        var mg = new GameObject("MenuYonetici");
        var hoparlor = mg.AddComponent<AudioSource>();
        hoparlor.playOnAwake = false;
        var m = mg.AddComponent<MenuYonetici>();
        Bagla(m, "anaPanel", ana);
        Bagla(m, "ayarlarPaneli", ayarP);
        Bagla(m, "sesYazisi", sesYazi);
        Bagla(m, "zorlukYazisi", zorYazi);
        Bagla(m, "hizYazisi", hizYazi);
        Bagla(m, "hoparlor", hoparlor);
        Bagla(m, "tikSesi", Ses("Tik"));

        UnityEventTools.AddPersistentListener(oyna.onClick, m.Oyna);
        UnityEventTools.AddPersistentListener(ayarlar.onClick, m.AyarlariAc);
        UnityEventTools.AddPersistentListener(geri.onClick, m.GeriDon);
        UnityEventTools.AddPersistentListener(sesBtn.onClick, m.SesDegistir);
        UnityEventTools.AddIntPersistentListener(kolay.onClick, m.ZorlukSec, 0);
        UnityEventTools.AddIntPersistentListener(zor.onClick, m.ZorlukSec, 1);
        UnityEventTools.AddIntPersistentListener(yavas.onClick, m.HizSec, 0);
        UnityEventTools.AddIntPersistentListener(normal.onClick, m.HizSec, 1);
        UnityEventTools.AddIntPersistentListener(hizli.onClick, m.HizSec, 2);

        EditorSceneManager.SaveScene(s, SAHNE + "Menu.unity");
    }

    static void OyunKur()
    {
        var s = YeniSahne(6f, ARKA);

        var tg = new GameObject("Top");
        tg.AddComponent<SpriteRenderer>().sprite = daire;
        var cc = tg.AddComponent<CircleCollider2D>();
        cc.radius = 0.5f;
        cc.isTrigger = true;
        var rb = tg.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        tg.AddComponent<Top>();
        tg.transform.localScale = new Vector3(0.6f, 0.6f, 1f);
        var topPrefab = PrefabUtility.SaveAsPrefabAsset(tg, "Assets/Prefabs/Top.prefab");
        Object.DestroyImmediate(tg);

        var sg = new GameObject("Sepet");
        var ssr = sg.AddComponent<SpriteRenderer>();
        ssr.sprite = sepetSprite;
        ssr.color = new Color(0.95f, 0.75f, 0.45f);
        sg.transform.position = new Vector3(0f, -4.8f, 0f);
        sg.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
        var bc = sg.AddComponent<BoxCollider2D>();
        bc.size = new Vector2(2.2f, 0.6f);
        bc.offset = new Vector2(0f, 0.25f);
        var sepet = sg.AddComponent<Sepet>();

        var zemin = new GameObject("Zemin");
        zemin.AddComponent<SpriteRenderer>().sprite = kare;
        zemin.GetComponent<SpriteRenderer>().color = new Color(0.05f, 0.12f, 0.13f);
        zemin.transform.position = new Vector3(0f, -5.9f, 0f);
        zemin.transform.localScale = new Vector3(20f, 0.6f, 1f);

        var c = CanvasKur();
        var puan = Yazi("PuanYazisi", c, "Puan: 0 / 10", 60, new Vector2(0f, 1f), new Vector2(230, -100), new Vector2(420, 110), Color.white);
        var can = Yazi("CanYazisi", c, "Can: 3", 60, new Vector2(1f, 1f), new Vector2(-180, -100), new Vector2(320, 110), new Color(1f, 0.5f, 0.5f));

        var sonuc = Panel("SonucPaneli", c, new Color(0, 0, 0, 0.75f));
        var sonucYazi = Yazi("SonucYazisi", sonuc.transform, "KAZANDIN!", 100, ORTA, new Vector2(0, 300), new Vector2(1000, 400), Color.white);
        var tekrar = Buton("Tekrar", sonuc.transform, "Tekrar Oyna", new Vector2(0, -100));
        var menu = Buton("Menu", sonuc.transform, "Menü", new Vector2(0, -300));

        var yg = new GameObject("OyunYonetici");
        var hoparlor = yg.AddComponent<AudioSource>();
        hoparlor.playOnAwake = false;
        var y = yg.AddComponent<OyunYonetici>();
        Bagla(y, "topPrefab", topPrefab.GetComponent<Top>());
        Bagla(y, "sepet", sepet);
        Bagla(y, "puanYazisi", puan);
        Bagla(y, "canYazisi", can);
        Bagla(y, "sonucPaneli", sonuc);
        Bagla(y, "sonucYazisi", sonucYazi);
        Bagla(y, "hoparlor", hoparlor);
        Bagla(y, "tutmaSesi", Ses("Tut"));
        Bagla(y, "kacirmaSesi", Ses("Kacti"));
        Bagla(y, "kazanmaSesi", Ses("Kazandin"));
        Bagla(y, "kaybetmeSesi", Ses("Kaybettin"));

        UnityEventTools.AddPersistentListener(tekrar.onClick, y.TekrarOyna);
        UnityEventTools.AddPersistentListener(menu.onClick, y.MenuyeDon);

        EditorSceneManager.SaveScene(s, SAHNE + "Oyun.unity");
    }

    static void DeneyMerhabaKur()
    {
        var s = YeniSahne(5f, new Color(0.2f, 0.2f, 0.25f));
        var g = new GameObject("Kare");
        g.AddComponent<SpriteRenderer>().sprite = kare;
        g.AddComponent<BoxCollider2D>().size = Vector2.one;
        g.transform.localScale = new Vector3(2f, 2f, 1f);
        g.AddComponent<Merhaba>();
        EditorSceneManager.SaveScene(s, SAHNE + "Deney_Merhaba.unity");
    }

    static void DeneyHareketKur()
    {
        var s = YeniSahne(5f, new Color(0.2f, 0.2f, 0.25f));
        var g = new GameObject("Sepetci");
        g.AddComponent<SpriteRenderer>().sprite = sepetSprite;
        g.AddComponent<Hareket>();
        EditorSceneManager.SaveScene(s, SAHNE + "Deney_Hareket.unity");
    }

    static void DeneySekmeKur()
    {
        var s = YeniSahne(5f, new Color(0.2f, 0.2f, 0.25f));
        var tg = new GameObject("Top");
        tg.AddComponent<SpriteRenderer>().sprite = daire;
        tg.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        tg.AddComponent<Sekme>();
        var topPrefab = PrefabUtility.SaveAsPrefabAsset(tg, "Assets/Prefabs/DeneyTop.prefab");
        Object.DestroyImmediate(tg);

        var u = new GameObject("TopUretici").AddComponent<TopUretici>();
        Bagla(u, "topPrefab", topPrefab.GetComponent<Sekme>());
        EditorSceneManager.SaveScene(s, SAHNE + "Deney_Sekme.unity");
    }

    static Sprite SpriteHazirla(string yol, int ppu)
    {
        var imp = (TextureImporter)AssetImporter.GetAtPath(yol);
        imp.textureType = TextureImporterType.Sprite;
        imp.spriteImportMode = SpriteImportMode.Single;
        imp.spritePixelsPerUnit = ppu;
        var ayar = new TextureImporterSettings();
        imp.ReadTextureSettings(ayar);
        ayar.spriteAlignment = (int)SpriteAlignment.Center;
        imp.SetTextureSettings(ayar);
        imp.SaveAndReimport();
        return AssetDatabase.LoadAssetAtPath<Sprite>(yol);
    }

    static Scene YeniSahne(float boyut, Color arka)
    {
        var s = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        var kg = new GameObject("Main Camera");
        kg.tag = "MainCamera";
        var k = kg.AddComponent<Camera>();
        k.orthographic = true;
        k.orthographicSize = boyut;
        k.clearFlags = CameraClearFlags.SolidColor;
        k.backgroundColor = arka;
        kg.AddComponent<AudioListener>();
        kg.transform.position = new Vector3(0, 0, -10);
        return s;
    }

    static Transform CanvasKur()
    {
        var go = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        go.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        var sc = go.GetComponent<CanvasScaler>();
        sc.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        sc.referenceResolution = new Vector2(1080, 1920);
        sc.matchWidthOrHeight = 0.5f;
        new GameObject("EventSystem", typeof(EventSystem), typeof(InputSystemUIInputModule));
        return go.transform;
    }

    static RectTransform UIObje(string ad, Transform ebeveyn, Vector2 anchor, Vector2 konum, Vector2 boyut)
    {
        var go = new GameObject(ad, typeof(RectTransform));
        var rt = go.GetComponent<RectTransform>();
        rt.SetParent(ebeveyn, false);
        rt.anchorMin = anchor;
        rt.anchorMax = anchor;
        rt.pivot = ORTA;
        rt.anchoredPosition = konum;
        rt.sizeDelta = boyut;
        return rt;
    }

    static TextMeshProUGUI Yazi(string ad, Transform eb, string metin, float punto, Vector2 anchor, Vector2 konum, Vector2 boyut, Color renk)
    {
        var t = UIObje(ad, eb, anchor, konum, boyut).gameObject.AddComponent<TextMeshProUGUI>();
        t.text = metin;
        t.fontSize = punto;
        t.alignment = TextAlignmentOptions.Center;
        t.color = renk;
        t.raycastTarget = false;
        return t;
    }

    static Image Resim(string ad, Transform eb, Sprite sp, Color renk, Vector2 anchor, Vector2 konum, Vector2 boyut)
    {
        var img = UIObje(ad, eb, anchor, konum, boyut).gameObject.AddComponent<Image>();
        img.sprite = sp;
        img.color = renk;
        img.preserveAspect = true;
        img.raycastTarget = false;
        return img;
    }

    static Button Buton(string ad, Transform eb, string metin, Vector2 konum)
    {
        return Buton(ad, eb, metin, konum, new Vector2(600, 140));
    }

    static Button Buton(string ad, Transform eb, string metin, Vector2 konum, Vector2 boyut)
    {
        var img = Resim(ad, eb, butonSprite, new Color(1f, 0.82f, 0.4f), ORTA, konum, boyut);
        img.preserveAspect = false;
        img.raycastTarget = true;
        img.type = Image.Type.Sliced;
        var b = img.gameObject.AddComponent<Button>();
        Yazi("Yazi", img.transform, metin, 58, ORTA, Vector2.zero, boyut, new Color(0.1f, 0.1f, 0.1f));
        return b;
    }

    static GameObject Panel(string ad, Transform eb, Color renk)
    {
        var rt = UIObje(ad, eb, ORTA, Vector2.zero, Vector2.zero);
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.sizeDelta = Vector2.zero;
        rt.gameObject.AddComponent<Image>().color = renk;
        return rt.gameObject;
    }

    static AudioClip Ses(string ad)
    {
        return AssetDatabase.LoadAssetAtPath<AudioClip>("Assets/Sesler/" + ad + ".wav");
    }

    static void Bagla(Object hedef, string alan, Object deger)
    {
        var so = new SerializedObject(hedef);
        var p = so.FindProperty(alan);
        if (p == null)
        {
            Debug.LogError(hedef.name + " içinde '" + alan + "' alanı bulunamadı");
            return;
        }
        p.objectReferenceValue = deger;
        so.ApplyModifiedPropertiesWithoutUndo();
    }
}
