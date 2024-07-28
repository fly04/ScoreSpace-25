using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] GameObject[] elements;
    [SerializeField] float[] spawnChances;
    [SerializeField] bool isStopped = false;

    private int currentIndex = 0;
    private GameObject lastObject = null;

    [SerializeField] SpiderController spider;

    int nothingCount = 0;

    [SerializeField] int maxNothing = 3;

    [SerializeField] GameController gameController;


    public bool isScruti = false;
    [SerializeField] GameObject scrutiPrefab;
    [SerializeField] GameObject scruti;

    void Start()
    {
        spider = GameObject.Find("Spider").GetComponent<SpiderController>();
    }

    void Update()
    {
        if (gameController.isPlaying)
        {
            handleInputs();

            if (!isStopped && spider.isAlive)
            {
                GameObject currentElement = elements[currentIndex];
                ElementController elementController = currentElement.GetComponent<ElementController>();
                float objectSpeed = elementController.moveSpeed;

                if (lastObject != null)
                {
                    Renderer lastObjectRenderer = lastObject.GetComponent<Renderer>();
                    float lastObjectEndPosition = lastObject.transform.position.x + (lastObjectRenderer.bounds.size.x / 2);
                    float distanceToLastObject = transform.position.x - lastObjectEndPosition;

                    if (distanceToLastObject > GetObjectWidth(currentElement) / 2)
                    {
                        SpawnPrefab();
                    }
                }
                else
                {
                    SpawnPrefab();
                }
            }
            handleScruti();
        }


    }

    void handleScruti()
    {
        if (!isScruti && isStopped)
        {
            Vector3 spawnPosition = new Vector3(0, 0, 0);
            scruti = Instantiate(scrutiPrefab, spawnPosition, Quaternion.identity);
            isScruti = true;
        }

        if (isScruti && !isStopped)
        {
            scruti.GetComponent<ScrutiController>().move();
            scruti.GetComponent<ScrutiController>().pauseAnimation();
        }

        if (isScruti && isStopped)
        {
            scruti.GetComponent<ScrutiController>().resumeAnimation();
        }

        if (isScruti && scruti.transform.position.x < -2)
        {
            Destroy(scruti);
            isScruti = false;
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

    GameObject getRandomPrefab()
    {
        float totalChance = 0;
        for (int i = 0; i < spawnChances.Length; i++)
        {
            totalChance += spawnChances[i];
        }
        float randomValue = Random.Range(0f, totalChance);
        float tempSum = 0;
        int prefabIndex = -1;
        for (int i = 0; i < elements.Length; i++)
        {
            tempSum += spawnChances[i];
            if (randomValue <= tempSum)
            {
                prefabIndex = i;
                break;
            }
        }

        if (prefabIndex == 0)
        {
            nothingCount++;
        }
        else
        {
            nothingCount = 0;
        }

        if (nothingCount >= maxNothing)
        {
            prefabIndex = Random.Range(1, elements.Length);
            nothingCount = 0;
        }

        return elements[prefabIndex];
    }

    void SpawnPrefab()
    {
        GameObject selectedPrefab = getRandomPrefab();

        // Récupérer la largeur de l'objet sélectionné
        float selectedPrefabWidth = GetObjectWidth(selectedPrefab);

        // Instancier l'objet sélectionné juste à côté de l'objet précédent
        Vector3 spawnPosition;
        if (lastObject != null)
        {
            Renderer lastObjectRenderer = lastObject.GetComponent<Renderer>();
            float lastObjectWidth = lastObjectRenderer.bounds.size.x;
            float lastObjectEndPosition = lastObject.transform.position.x + (lastObjectWidth / 2);
            spawnPosition = new Vector3(lastObjectEndPosition + (selectedPrefabWidth / 2), transform.position.y, transform.position.z);
        }
        else
        {
            spawnPosition = new Vector3(transform.position.x + (selectedPrefabWidth / 2), transform.position.y, transform.position.z);
        }
        lastObject = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
    }

    private float GetObjectWidth(GameObject prefab)
    {
        Renderer objectRenderer = prefab.GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            return objectRenderer.bounds.size.x;
        }
        return 0f;
    }


}
