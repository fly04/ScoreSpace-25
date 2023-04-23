using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using LootLocker.Requests;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int score = 0;
    [SerializeField] Leaderboard leaderboard;


    // void Start()
    // {
    //     StartCoroutine(SetupRoutine());
    // }

    // IEnumerator SetupRoutine()
    // {
    //     yield return StartCoroutine(LoginRoutine());
    //     yield return StartCoroutine(leaderboard.FetchTopHighScoresRoutine());
    // }

    // IEnumerator LoginRoutine()
    // {
    //     bool done = false;
    //     LootLockerSDKManager.StartGuestSession((response) =>
    //     {
    //         if (response.success)
    //         {
    //             Debug.Log("Successfully logged in");
    //             PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
    //             done = true;
    //         }
    //         else
    //         {
    //             Debug.Log("Error logging in");
    //             done = true;
    //         }
    //     });
    //     yield return new WaitWhile(() => !done);
    // }

    // public IEnumerator SubmitScoreRoutine(int scoreToUpload)
    // {
    //     Debug.Log("Submitting score");
    //     bool done = false;
    //     string playerId = PlayerPrefs.GetString("PlayerID");
    //     LootLockerSDKManager.SubmitScore(playerId, scoreToUpload, leaderboard.id, (response) =>
    //     {
    //         if (response.success)
    //         {
    //             Debug.Log("Successfully submitted score");
    //             done = true;
    //         }
    //         else
    //         {
    //             Debug.Log("Error submitting score" + response.Error);
    //             done = true;
    //         }
    //     });
    //     yield return new WaitWhile(() => !done);

    //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    // }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "Score: " + score);
    }
}