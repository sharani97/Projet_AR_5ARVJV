using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;


public class Player : MonoBehaviour {
    private int score=0;
    

    [PunRPC]
    public void AddScore(int newScore) {
        score += newScore;
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("UpdateScore", RpcTarget.All, PhotonNetwork.NickName, score);
    }

    [PunRPC]
    void UpdateScore(string playerName, int newScore) {
        ScoreManager scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        PlayerScore playerScore = new PlayerScore {
            playerName = playerName,
            score = newScore
        };
        int index = scoreManager.playerScores.FindIndex(x => x.playerName == playerName);
        if (index != -1) {
            scoreManager.playerScores[index] = playerScore;
        } else {
            scoreManager.playerScores.Add(playerScore);
        }
        scoreManager.playerScores.Sort((x, y) => y.score.CompareTo(x.score));
    }
}

