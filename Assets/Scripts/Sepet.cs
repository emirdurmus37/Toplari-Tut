using UnityEngine;
using UnityEngine.InputSystem;

public class Sepet : MonoBehaviour
{
    public bool hareketEdebilir = true;
    float hiz;
    float sinir;

    void Start()
    {
        int secim = PlayerPrefs.GetInt("sepetHizi", 1);
        if (secim == 0)
        {
            hiz = 6f;
        }
        else if (secim == 1)
        {
            hiz = 10f;
        }
        else
        {
            hiz = 15f;
        }

        float yarimGenislik = GetComponent<SpriteRenderer>().bounds.extents.x;
        sinir = Camera.main.orthographicSize * Camera.main.aspect - yarimGenislik;
    }

    void Update()
    {
        if (!hareketEdebilir) return;

        var isaret = Pointer.current;
        if (isaret == null || !isaret.press.isPressed) return;

        Vector2 dunya = Camera.main.ScreenToWorldPoint(isaret.position.ReadValue());
        float hedefX = Mathf.Clamp(dunya.x, -sinir, sinir);
        Vector3 hedef = new Vector3(hedefX, transform.position.y, 0f);

        transform.position = Vector3.MoveTowards(transform.position, hedef, hiz * Time.deltaTime);
    }
}
