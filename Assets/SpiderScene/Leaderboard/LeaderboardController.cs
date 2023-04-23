// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;
// using LootLocker.Requests;

// public class LeaderboardController : MonoBehaviour
// {
//     [SerializeField] TextMeshProUGUI playerNames;   //là où vont être affichés les noms des joueurs du leaderboard
//     [SerializeField] TextMeshProUGUI playerScores;   //là où vont être affichés les scores des joueurs du leaderboard

//     [SerializeField] TextMeshProUGUI playerName;   //là où va être affiché le nom du joueur
//     [SerializeField] TextMeshProUGUI playerScore;   //là où va être affiché le score du joueur
//     public string id = "13575"; //id du leaderboard

//     [SerializeField] bool isDebug;

//     public IEnumerator FetchTopHighScoresRoutine()
//     {
//         if (isDebug) Debug.Log("Fetching high scores");
//         bool done = false;
//         LootLockerSDKManager.GetScoreList(id, 5, 0, response =>
//         {
//             if (response.success)
//             {
//                 string tempPlayerNames = "Names\n";
//                 string tempPlayerScores = "Scores\n";

//                 LootLockerLeaderboardMember[] members = response.items;

//                 for (int i = 0; i < members.Length; i++)
//                 {
//                     tempPlayerNames += i + 1 + ". ";
//                     //soit c'est le joueur
//                     if (members[i].member_id == PlayerPrefs.GetString("PlayerID"))
//                     {
//                         tempPlayerNames += "<color=#FF0000>";
//                         tempPlayerNames += "You";
//                         tempPlayerNames += "</color>";
//                     }
//                     else
//                     {
//                         tempPlayerNames += "Player #" + members[i].player.id;
//                     }
//                     tempPlayerScores += members[i].score + "\n";
//                     tempPlayerNames += "\n";
//                 }
//                 done = true;
//                 playerNames.text = tempPlayerNames;
//                 playerScores.text = tempPlayerScores;
//             }
//             else
//             {
//                 if (isDebug) Debug.Log("Error fetching high scores");
//                 done = true;
//             }
//         });

//         yield return new WaitWhile(() => !done);
//     }

//     public IEnumerator FetchPlayerRankRoutine()
//     {
//         if (isDebug) Debug.Log("Fetching player rank");
//         bool done = false;
//         string playerId = PlayerPrefs.GetString("PlayerID");
//         LootLockerSDKManager.GetMemberRank(id, playerId, response =>
//         {
//             if (response.success)
//             {
//                 if (isDebug)
//                 {
//                     Debug.Log("Successfully fetched player rank");
//                     Debug.Log(response);
//                 }
//                 playerName.text = response.rank + ". You";
//                 playerScore.text = response.score.ToString();
//                 done = true;
//             }
//             else
//             {
//                 if (isDebug) Debug.Log("Error fetching player rank");
//                 done = true;
//             }
//         });
//         yield return new WaitWhile(() => !done);
//     }
// }
