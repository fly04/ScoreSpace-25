using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlescreenController : MonoBehaviour
{
    bool hasBeenStarted = false;
    GameController gameController;
    Animator animator;

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        animator = GetComponent<Animator>();
    }

    void OnMouseDown()
    {
        {
            if (!hasBeenStarted)
            {
                hasBeenStarted = true;
                animator.CrossFade("start", 0.1f);
                StartCoroutine(moveCamera());
            }
        }

        IEnumerator moveCamera()
        {
            yield return new WaitForSeconds(1);
            gameController.titlescreenToGame();
            StartCoroutine(startGame());
        }

        IEnumerator startGame()
        {
            yield return new WaitForSeconds(1);
            gameController.isPlaying = true;
        }
    }
}