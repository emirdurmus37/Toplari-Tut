using UnityEngine;

public class Top : MonoBehaviour
{
    public float hiz = 3f;
    public OyunYonetici yonetici;
    float altSinir;

    void Start()
    {
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.down * hiz;
        altSinir = -Camera.main.orthographicSize - 1f;
    }

    void Update()
    {
        if (transform.position.y < altSinir)
        {
            yonetici.TopKacti();
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D diger)
    {
        if (diger.GetComponent<Sepet>() != null)
        {
            yonetici.TopTutuldu();
            Destroy(gameObject);
        }
    }
}
