using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class PlayerScore
{
    public string playerName;
    public int score;
}
public class ScoreManager : MonoBehaviour
{
   public List<PlayerScore> playerScores = new List<PlayerScore>();
   
   public void AddScore(string playerName, int score) {
       PlayerScore playerScore = new PlayerScore {
           playerName = playerName,
           score = score
       };
       int index = playerScores.FindIndex(x => x.playerName == playerName);
       if (index != -1) {
           playerScores[index] = playerScore;
       } else {
           playerScores.Add(playerScore);
       }
       playerScores.Sort((x, y) => y.score.CompareTo(x.score));
   }
}
