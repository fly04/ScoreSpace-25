using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{

    [SerializeField] GameObject[] elements;
    [SerializeField] float[] spawnChances;
    [SerializeField] float spawnRate = 2;
    [SerializeField] bool isStopped = false;

    private float timer = 0;

    void Start()
    {
        SpawnPrefab();
    }

    void Update()
    {
        handleInputs();

        if (!isStopped)
        {
            if (timer < spawnRate)
            {
                timer += Time.deltaTime;
            }
            else
            {
                SpawnPrefab();
                timer = 0;
            }
        }
    }


    void handleInputs()
    {
        if (Input.GetMouseButton(0))
        {
            isStopped = true;
        }
        else
        {
            isStopped = false;
        }
    }
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
