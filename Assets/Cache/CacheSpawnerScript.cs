using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacheSpawnerScript : MonoBehaviour
{

    public GameObject cache;
    public float spawnRate = 2;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        SpawnCache();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            SpawnCache();
            timer = 0;
        }

        
    }

    void SpawnCache()
    {
        Instantiate(cache, transform.position, transform.rotation);
    }
}
