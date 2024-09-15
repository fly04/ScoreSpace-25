using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenaceController : MonoBehaviour
{
    Animator animator;
    [SerializeField] SpiderController spider;
    [SerializeField] bool isDebug;
    SpriteRenderer spriteRenderer;
    GameController gameController;

    [SerializeField] float minWait = 1f;
    [SerializeField] float maxWait = 5f;

    // [SerializeField] int id = 0;

    bool hasHit = false;



    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        spider = GameObject.Find("Spider").GetComponent<SpiderController>();

        StartCoroutine(Initialize());
    }

    void Update()
    {
        if (!spider.isAlive && !hasHit)
        {
            StopAllCoroutines();
            animator.CrossFade("leave", 0.1f);
        }
    }

    IEnumerator Initialize()
    {
        yield return new WaitForSeconds(3);
        StartCoroutine(WaitBeforeSneak());
    }

    IEnumerator WaitBeforeSneak()
    {
        if (isDebug) Debug.Log("waiting before sneak");

        spriteRenderer.sortingOrder = -10;

        //50% chances to flipX
        if (Random.Range(0, 2) == 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        yield return new WaitForSeconds(Random.Range(minWait, maxWait));

        StartCoroutine(Sneak());
    }

    IEnumerator Sneak()
    {
        if (isDebug) Debug.Log("sneaking");

        animator.CrossFade("sneak", 0.1f);

        yield return new WaitForSeconds(1.5f);

        if (spider.isSafe)
        {
            StartCoroutine(Leave());
        }
        else
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Leave()
    {
        if (isDebug) Debug.Log("leaving");

        animator.CrossFade("leave", 0.1f);
        yield return new WaitForSeconds(.5f);

        StartCoroutine(WaitBeforeSneak());
        yield return null;
    }

    IEnumerator Attack()
    {
        if (isDebug) Debug.Log("attacking");

        spriteRenderer.sortingOrder = 10;
        animator.CrossFade("slap", 0.1f);

        spider.Die();

        StartCoroutine(gameController.gameToLeaderboard());
        yield return null;
    }
}
