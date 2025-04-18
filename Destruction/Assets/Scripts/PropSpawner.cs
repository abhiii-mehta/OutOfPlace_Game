using UnityEngine;

public class PropSpawner : MonoBehaviour
{
    public GameObject realPropPrefab;  
    public GameObject fakePropPrefab;  

    public int totalProps = 25;
    public int fakePropsCount = 8;
    public float spawnWidth = 9f;

    void Start()
    {
        SpawnProps();
    }

    void SpawnProps()
    {
        int fakeLeft = fakePropsCount;

        for (int i = 0; i < totalProps; i++)
        {
            float randomX = Random.Range(-spawnWidth / 2f, spawnWidth / 2f);
            Vector3 spawnPos = new Vector3(randomX, transform.position.y, 0f);

            bool isFake = false;
            if (fakeLeft > 0 && Random.value < ((float)fakeLeft / (totalProps - i)))
            {
                isFake = true;
                fakeLeft--;
            }

            GameObject prefabToUse = isFake ? fakePropPrefab : realPropPrefab;
            GameObject prop = Instantiate(prefabToUse, spawnPos, Quaternion.identity);

            PropBehavior behavior = prop.GetComponent<PropBehavior>();
            if (behavior != null)
            {
                behavior.isFake = isFake;
            }
            if (!isFake)
            {
                GameManager.instance?.RegisterRealProp();
            }

        }
    }
}
