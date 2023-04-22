using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{

    [SerializeField] GameObject[] elements;
    [SerializeField] float[] spawnChances;
    [SerializeField] float spawnRate = 2;

    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        // SpawnCache();
        SpawnPrefab();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            // SpawnCache();
            SpawnPrefab();
            timer = 0;
        }
    }

    // void SpawnCache()
    // {
    //     Instantiate(cache, transform.position, transform.rotation);
    // }

    public void SpawnPrefab()
    {
        float totalChance = 0;
        for (int i = 0; i < spawnChances.Length; i++)
        {
            totalChance += spawnChances[i];
        }
        float randomValue = Random.Range(0f, totalChance);
        float tempSum = 0;
        for (int i = 0; i < elements.Length; i++)
        {
            tempSum += spawnChances[i];
            if (randomValue <= tempSum)
            {
                Instantiate(elements[i], transform.position, Quaternion.identity);
                break;
            }
        }
    }
}
