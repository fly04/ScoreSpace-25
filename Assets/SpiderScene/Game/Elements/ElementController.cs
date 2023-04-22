using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementController : MonoBehaviour
{

    [SerializeField] ScoresManager scoresManager;
    [SerializeField] bool isStopped = false;
    public float moveSpeed = 5;
    bool hasBeenCounted = false;

    void Start()
    {
        scoresManager = GameObject.Find("ScoresManager").GetComponent<ScoresManager>();
    }

    // Update is called once per frame
    void Update()
    {
        handleInputs();
        move();
    }

    void move()
    {
        if (!isStopped)
        {
            transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;
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

    void checkAddScore()
    {
        if (!hasBeenCounted)
        {
            if (transform.position.x < 0)
            {
                hasBeenCounted = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            scoresManager.addScore(1);
        }
    }
}
