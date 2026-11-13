using UnityEngine;
using UnityEngine.InputSystem;

public class Merhaba : MonoBehaviour
{
    int sayac = 0;

    void Start()
    {
        Debug.Log("Selam, ben " + gameObject.name);
    }

    void Update()
    {
        var isaret = Pointer.current;
        if (isaret == null || !isaret.press.wasPressedThisFrame) return;

        Vector2 dunya = Camera.main.ScreenToWorldPoint(isaret.position.ReadValue());

        if (GetComponent<Collider2D>().OverlapPoint(dunya))
        {
            sayac = sayac + 1;
            Debug.Log("Dokunuldu: " + sayac);

            GetComponent<SpriteRenderer>().color = Random.ColorHSV();
        }
    }
}
