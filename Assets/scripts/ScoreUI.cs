using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TMP_Text scoreTextPrefab;

    void Start() {
        ScoreManager scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        foreach (PlayerScore playerScore in scoreManager.playerScores) {
            TMP_Text scoreText = Instantiate(scoreTextPrefab);
            scoreText.text = playerScore.playerName + ": " + playerScore.score;
            scoreText.transform.SetParent(transform);
        }
    }
}