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

    void Start()
    {
        SpawnPrefab();
    }

    void Update()
    {
        handleInputs();

        if (!isStopped)
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
        if (lastObject != null)
        {
            if (lastObject.tag == "Menace")
            {
                if (lastObject.GetComponent<MenaceController>().type == 0) lastObject.GetComponent<MenaceController>().isActive = true;
            }
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
