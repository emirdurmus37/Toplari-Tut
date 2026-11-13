using UnityEngine;

public class Hareket : MonoBehaviour
{
    [SerializeField] float hiz = 2f;
    [SerializeField] bool aktif = true;
    [SerializeField] int can = 3;
    [SerializeField] string ad = "Sepetçi";
    [SerializeField] float solSinir = -8f;

    void Start()
    {
        Debug.Log(ad + " oyuna girdi. Can: " + can);
    }

    void Update()
    {
        if (!aktif) return;

        transform.position += Vector3.left * hiz * Time.deltaTime;

        if (transform.position.x < solSinir)
        {
            transform.position = new Vector3(-solSinir, transform.position.y, 0f);
        }
    }

    public void CanKaybet()
    {
        can = can - 1;
        Debug.Log(ad + " can kaybetti. Kalan: " + can);
    }
}
