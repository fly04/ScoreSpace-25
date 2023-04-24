using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public float multiplier = 1;
    public bool isPlaying = false;

    [SerializeField] CameraController cameraController;

    [SerializeField] GameObject[] menaces;

    [SerializeField] ScoresManager scoresManager;

    void Start()
    {
        scoresManager = GameObject.Find("ScoresManager").GetComponent<ScoresManager>();
    }

    void FixedUpdate()
    {
        if (multiplier < 3)
            multiplier += 0.0001f;
    }

    public IEnumerator gameToLeaderboard()
    {
        StartCoroutine(scoresManager.RefreshLeaderboardRoutine());
        yield return new WaitForSeconds(1);
        cameraController.gameToLeaderboard();
    }

    public void titlescreenToGame()
    {
        cameraController.titlescreenToGame();
    }

    public void spawnMenaces()
    {
        foreach (GameObject menace in menaces)
        {
            Instantiate(menace, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}
