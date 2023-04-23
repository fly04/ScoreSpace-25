using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public float multiplier = 1;
    [SerializeField] GameObject cam;

    void FixedUpdate()
    {
        if (multiplier < 3)
            multiplier += 0.0001f;
    }

    public void goToLeaderboard()
    {
        cam.GetComponent<Animator>().CrossFade("GameToLeaderboard", 0.1f);
    }
}
