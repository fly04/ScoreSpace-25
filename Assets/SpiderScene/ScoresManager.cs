using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class ScoresManager : MonoBehaviour
{
    public int score = 0;
    [SerializeField] LeaderboardController leaderboard;
    [SerializeField] TextMeshProUGUI[] scores;
    [SerializeField] bool isDebug;

    void Start()
    {
        StartCoroutine(InitRoutine());
    }

    IEnumerator InitRoutine()
    {
        yield return StartCoroutine(LoginRoutine());
        yield return StartCoroutine(leaderboard.FetchTopHighScoresRoutine());
        yield return StartCoroutine(leaderboard.FetchPlayerRankRoutine());
    }

    public IEnumerator RefreshLeaderboardRoutine()
    {
        yield return StartCoroutine(SubmitScoreRoutine(score));
        yield return StartCoroutine(leaderboard.FetchTopHighScoresRoutine());
        yield return StartCoroutine(leaderboard.FetchPlayerRankRoutine());
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
    }

    public void addScore(int scoreToAdd)
    {
        score += scoreToAdd;
        for (int i = 0; i < scores.Length; i++)
        {
            scores[i].text = score.ToString();
        }
    }
}
