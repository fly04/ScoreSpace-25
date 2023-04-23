using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public float multiplier = 1;
    [SerializeField] GameObject camera;

    void FixedUpdate()
    {
        if (multiplier < 3)
            multiplier += 0.0001f;
    }

    public void goToLeaderboard()
    {
        camera.GetComponent<Animator>().CrossFade("GameToLeaderboard", 0.1f);
    }
}
