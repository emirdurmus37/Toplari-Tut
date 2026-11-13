using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Splash : MonoBehaviour
{
    [SerializeField] float bekleme = 2f;
    float sayac = 0f;

    void Update()
    {
        sayac = sayac + Time.deltaTime;

        bool dokundu = Pointer.current != null && Pointer.current.press.wasPressedThisFrame;

        if (sayac >= bekleme || dokundu)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
