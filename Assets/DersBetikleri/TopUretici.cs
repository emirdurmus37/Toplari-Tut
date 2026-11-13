using UnityEngine;

public class TopUretici : MonoBehaviour
{
    [SerializeField] Sekme topPrefab;
    [SerializeField] int adet = 5;

    void Start()
    {
        for (int i = 0; i < adet; i++)
        {
            Vector3 yer = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0f);
            Sekme t = Instantiate(topPrefab, yer, Quaternion.identity);
            t.hiz = Random.insideUnitCircle.normalized * Random.Range(1.5f, 4f);

            var sr = t.GetComponent<SpriteRenderer>();
            if (i % 2 == 0)
            {
                sr.color = Color.yellow;
            }
            else
            {
                sr.color = Color.cyan;
            }
        }
    }
}
