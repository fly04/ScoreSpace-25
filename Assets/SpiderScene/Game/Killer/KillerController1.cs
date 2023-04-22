using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerController1 : MonoBehaviour
{

    [SerializeField] bool isStopped = false;
    [SerializeField] Animator animator;
    Vector3 initialPosition;

    [SerializeField] bool isAttacking = false;
    [SerializeField] float speed = 15f;
    [SerializeField] GameObject target;
    [SerializeField] float limitRight = 1.7f;
    [SerializeField] float limitLeft = -1.3f;
    GameObject previousTarget;

    void Start()
    {
        animator = GetComponent<Animator>();
        initialPosition = transform.position;
    }

    void Update()
    {
        handleInputs();

        if (target == null)
        {
            isAttacking = false;
            goToInitialPosition();
            StartCoroutine(getTarget());

        }
        else
        {
            followTarget();
            loseTarget();
        }

        if (!isAttacking)
        {
            if (isStopped) animator.CrossFade("Idle", 0.1f);
        }
    }

    void loseTarget()
    {
        if (target.transform.position.x < -2)
        {
            // Debug.Log("Lost target");
            target.GetComponent<MenaceController>().isActive = false;
            animator.CrossFade("Run", 0.1f);
            target = null;
        }
    }

    void goToInitialPosition()
    {
        if (transform.position.x < initialPosition.x)
        {
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > limitRight)
        {
            transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
    }

    void followTarget()
    {
        Vector3 direction = target.transform.position - transform.position;
        transform.position = new Vector3(transform.position.x + direction.x * speed * Time.deltaTime, transform.position.y, transform.position.z);
        // Debug.Log(direction.magnitude);
        if (transform.position.x - target.transform.position.x < 0.5f && transform.position.x - target.transform.position.x > -0.5f)
        {
            transform.position = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
            attack();
        }
    }

    void attack()
    {
        Debug.Log("Attacking");
        if (isAttacking) return;
        switch (target.GetComponent<MenaceController>().type)
        {
            case 0:
                animator.SetTrigger("Slap");
                break;
        }
        isAttacking = true;
        StartCoroutine(leaveTarget());
    }

    IEnumerator getTarget()
    {
        yield return new WaitForSeconds(0);

        GameObject[] menaces = GameObject.FindGameObjectsWithTag("Menace");
        GameObject target = null;
        float distance = 0;
        foreach (GameObject menace in menaces)
        {
            if (target == null)
            {
                if (menace.transform.position.x > -2 && menace.GetComponent<MenaceController>().isActive && previousTarget != menace)
                {
                    target = menace;
                    distance = Vector3.Distance(transform.position, menace.transform.position);
                }
            }
            else
            {
                float newDistance = Vector3.Distance(transform.position, menace.transform.position);
                if (newDistance < distance && target.transform.position.x > 0)
                {
                    if (menace.transform.position.x > -2 && menace.GetComponent<MenaceController>().isActive)
                    {
                        target = menace;
                        distance = newDistance;
                    }
                }
            }
        }
        this.target = target;
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

    //set isAttacking false a short random time
    IEnumerator leaveTarget()
    {
        previousTarget = target;
        yield return new WaitForSeconds(Random.Range(1f, 3f));

        if (previousTarget == target)
        {
            animator.CrossFade("Run", 0.1f);
            target = null;
            Debug.Log("Leaving target");
        }
    }


}
