using UnityEngine;

public class Sekme : MonoBehaviour
{
    public Vector2 hiz = new Vector2(2f, 3f);
    Vector2 sinir;

    void Start()
    {
        Camera k = Camera.main;
        sinir = new Vector2(k.orthographicSize * k.aspect, k.orthographicSize);
    }

    void Update()
    {
        Vector3 p = transform.position + (Vector3)(hiz * Time.deltaTime);

        if (p.y > sinir.y || p.y < -sinir.y) hiz.y = -hiz.y;
        if (p.x > sinir.x || p.x < -sinir.x) hiz.x = -hiz.x;

        transform.position = p;
    }
}
