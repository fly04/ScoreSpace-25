using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuyController : MonoBehaviour
{
    Animator animator;
    [SerializeField] SpiderController spider;
    [SerializeField] bool isDebug;
    SpriteRenderer spriteRenderer;
    GameController gameController;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        animator = GetComponent<Animator>();
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
        yield return new WaitForSeconds(3);
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
        spider.die();
        animator.CrossFade("slap", 0.1f);
        yield return new WaitForSeconds(1);
        gameController.goToLeaderboard();
    }
}
