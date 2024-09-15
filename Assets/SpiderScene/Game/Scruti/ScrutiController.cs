using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrutiController : MonoBehaviour
{
    [SerializeField] Animator animator;
    public float moveSpeed = 2;
    GameController gameController;
    [SerializeField] SpiderController spider;
    bool isAttacking = false;

    AudioSource audioSource;

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        spider = GameObject.Find("Spider").GetComponent<SpiderController>();
        audioSource = GetComponent<AudioSource>();
    }

    public void move()
    {
        if (isAttacking) return;
        transform.position = transform.position + (Vector3.left * moveSpeed * gameController.multiplier) * Time.deltaTime;
    }

    public void pauseAnimation()
    {
        if (isAttacking) return;
        if (animator.speed != 0) animator.speed = 0;
    }

    public void resumeAnimation()
    {
        if (isAttacking) return;
        if (animator.speed != 1) animator.speed = 1;
        if (transform.position.x < 0)
        {
            transform.position = transform.position + (Vector3.right * moveSpeed * gameController.multiplier) * Time.deltaTime;
        }
        else
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }
    }

    public void attack()
    {
        isAttacking = true;
        spider.Die();
        animator.CrossFade("attack", 0.1f);
        StartCoroutine(gameController.gameToLeaderboard());
    }

    public void playAudio()
    {
        audioSource.Play();
    }
}
