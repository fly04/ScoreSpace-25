using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerController : MonoBehaviour
{

    [SerializeField] bool isStopped = false;
    [SerializeField] Animator animator;
    [SerializeField] string lastAnim;

    void Start()
    {
        animator = GetComponent<Animator>();
        lastAnim = "Run";
    }

    void Update()
    {
        handleInputs();
        handleAnimation();
    }

    void handleAnimation()
    {
        if (isStopped)
        {
            animator.CrossFade("Idle", 0.1f);
        }
        else
        {
            animator.CrossFade(lastAnim, 0.1f);
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


}
