using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
// using LootLocker.Requests;

public class Leaderboard : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI playerNames;
    [SerializeField] TextMeshProUGUI playerScores;
    public string id = "13575";

    // public IEnumerator FetchTopHighScoresRoutine()
    // {
    //     Debug.Log("Fetching high scores");
    //     bool done = false;
    //     LootLockerSDKManager.GetScoreList(id, 10, 0, response =>
    //     {
    //         if (response.success)
    //         {
    //             string tempPlayerNames = "Names\n";
    //             string tempPlayerScores = "Scores\n";

    //             LootLockerLeaderboardMember[] members = response.items;

    //             for (int i = 0; i < members.Length; i++)
    //             {
    //                 // tempPlayerNames += members[i].player.name + ". ";
    //                 if (members[i].player.name != "")
    //                 {
    //                     tempPlayerNames += members[i].player.name;
    //                 }
    //                 else
    //                 {
    //                     tempPlayerNames += "Player #" + members[i].player.id;
    //                 }
    //                 tempPlayerScores += members[i].score + "\n";
    //                 tempPlayerNames += "\n";
    //             }
    //             done = true;
    //             playerNames.text = tempPlayerNames;
    //             playerScores.text = tempPlayerScores;
    //         }
    //         else
    //         {
    //             Debug.Log("Error fetching high scores");
    //             done = true;
    //         }
    //     });

    //     yield return new WaitWhile(() => !done);
    // }

}