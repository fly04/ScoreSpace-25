using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.SceneManagement;

public class ScoresManager : MonoBehaviour
{
    public int score = 0;
    [SerializeField] Leaderboard leaderboard;
    [SerializeField] bool isDebug;

    void Start()
    {
        // Identify the player
        StartCoroutine(LoginRoutine());

        // Fetch the top scores
        StartCoroutine(leaderboard.FetchTopHighScoresRoutine());
    }

    IEnumerator LoginRoutine()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                if (isDebug) Debug.Log("Successfully logged in");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            }
            else
            {
                if (isDebug) Debug.Log("Error logging in");
                done = true;
            }
        });
        yield return new WaitWhile(() => !done);
    }

    public IEnumerator SubmitScoreRoutine(int scoreToUpload)
    {
        if (isDebug) Debug.Log("Submitting score");
        bool done = false;
        string playerId = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.SubmitScore(playerId, scoreToUpload, leaderboard.id, (response) =>
        {
            if (response.success)
            {
                if (isDebug) Debug.Log("Successfully submitted score");
                done = true;
            }
            else
            {
                if (isDebug) Debug.Log("Error submitting score" + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => !done);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void addScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }
}
