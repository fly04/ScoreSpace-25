using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameController gameController;
    Animator animator;

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        animator = GetComponent<Animator>();
    }

    public void spawnMenaces()
    {
        gameController.spawnMenaces();
    }

    public void gameToLeaderboard()
    {
        animator.CrossFade("GameToLeaderboard", 0.1f);
    }

    public void titlescreenToGame()
    {
        animator.CrossFade("TitlescreenToGame", 0.1f);
    }
}
