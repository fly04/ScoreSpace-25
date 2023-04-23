using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] float verticalAmplitude = 0.1f;
    [SerializeField] float verticalFrequency = 2f;

    [Header("Debug")]
    Rigidbody2D rb;
    Vector3 startPosition;
    [SerializeField] bool isStopped = false;
    int time = 0;
    GameController gameController;

    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        startPosition = transform.position;
    }

    void Update()
    {
        handleInputs();
    }

    void FixedUpdate()
    {
        move();
        if (!isStopped)
        {
            time++;
            animator.CrossFade("run", 0.1f);
        }
        else animator.CrossFade("wait", 0.1f);
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

    void move()
    {
        if (!isStopped)
        {
            float newYPosition = startPosition.y + Mathf.Sin(time * verticalFrequency * gameController.multiplier) * verticalAmplitude;
            transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
        }
    }
}
