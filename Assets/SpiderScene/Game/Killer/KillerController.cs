using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerController : MonoBehaviour
{

    [SerializeField] bool isStopped = false;
    [SerializeField] Animator animator;
    [SerializeField] string lastAnim;

    [SerializeField] bool hasTarget = false;
    [SerializeField] bool isAttacking = false;
    [SerializeField] float speed = 15f;
    [SerializeField] GameObject target;

    void Start()
    {
        animator = GetComponent<Animator>();
        lastAnim = "Run";
    }

    void Update()
    {
        if (target != null)
        {
            hasTarget = true;
        }
        else
        {
            hasTarget = false;
            isAttacking = false;
        }


        handleInputs();
        handleAnimationStop();
        handleStates();
        constrainPosition();
    }

    void handleStates()
    {
        if (hasTarget)
        {
            followTarget(target);
            // attack(target);
            loseTarget();
        }
        else
        {
            target = getTarget();
            goMiddle();
        }
        if (!isAttacking)
        {
            animator.SetTrigger("Run");
        }
    }

    void handleAnimationStop()
    {
        if (isStopped && animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            animator.CrossFade("Idle", 0.1f);
        }
    }

    void goMiddle()
    {
        if (transform.position.x < 0)
        {
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        }

    }

    void constrainPosition()
    {
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, -1.3f, 1.7f);
        transform.position = position;
    }

    void attack(GameObject target)
    {
        if (isAttacking) return;
        if (target.GetComponent<MenaceController>().type == 0 && target.transform.position.x < 1.5)
        {
            animator.SetTrigger("Slap");
            isAttacking = true;
        }
    }

    GameObject getTarget()
    {
        GameObject[] menaces = GameObject.FindGameObjectsWithTag("Menace");
        GameObject target = null;
        float distance = 0;
        foreach (GameObject menace in menaces)
        {
            if (target == null)
            {
                target = menace;
                distance = Vector3.Distance(transform.position, menace.transform.position);
            }
            else
            {
                float newDistance = Vector3.Distance(transform.position, menace.transform.position);
                if (newDistance < distance && target.transform.position.x > 0)
                {
                    target = menace;
                    distance = newDistance;
                }
            }
        }
        return target;
    }

    void loseTarget()
    {
        if (target.transform.position.x < -1)
        {
            target = null;
        }
    }

    void followTarget(GameObject target)
    {

        Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
        Vector3 currentPosition = transform.position;
        Vector3 direction = targetPosition - currentPosition;
        float difference = direction.magnitude;
        if (difference < 0.5f)
        {
            direction.Normalize();
            transform.position = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
            attack(target);
        }
        else
        {
            transform.position = new Vector3(transform.position.x + (direction.x * speed * Time.deltaTime), transform.position.y, transform.position.z);
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
