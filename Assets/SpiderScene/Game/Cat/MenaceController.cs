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

    [SerializeField] int id = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        spider = GameObject.Find("Spider").GetComponent<SpiderController>();
        animator = GetComponent<Animator>();
        StartCoroutine(initialize());
    }

    void Update()
    {
        if (!spider.isAlive && spider.killer != id)
        {
            StopAllCoroutines();
            animator.CrossFade("leave", 0.1f);
        }
    }

    IEnumerator initialize()
    {
        yield return new WaitForSeconds(3);
        StartCoroutine(waitBeforeSneak());
    }

    IEnumerator waitBeforeSneak()
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
        StartCoroutine(sneak());
    }

    IEnumerator sneak()
    {
        if (isDebug) Debug.Log("sneaking");
        animator.CrossFade("sneak", 0.1f);
        yield return new WaitForSeconds(1.5f);

        if (spider.isSafe)
        {
            StartCoroutine(leave());
        }
        else
        {
            StartCoroutine(attack());
        }
    }

    IEnumerator leave()
    {
        if (isDebug) Debug.Log("leaving");
        animator.CrossFade("leave", 0.1f);
        yield return new WaitForSeconds(.5f);
        StartCoroutine(waitBeforeSneak());
        yield return null;
    }

    IEnumerator attack()
    {
        if (isDebug) Debug.Log("attacking");
        spriteRenderer.sortingOrder = 10;
        spider.killer = id;
        spider.die();
        animator.CrossFade("slap", 0.1f);
        yield return new WaitForSeconds(1);
        gameController.gameToLeaderboard();
    }
}
