using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI timeLefText;
    public TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.OnScoreUpdated += UpdateScore;
            GameManager.Instance.OnTimeLeftUpdated += UpdateTimeLeft;
        }
    }

    private void UpdateScore(float score)
    {
        // Round the score to the nearest 50 units
        int roundedScore = Mathf.RoundToInt(score / 50) * 50;
        scoreText.text = roundedScore + " $";
    }

    private void UpdateTimeLeft(float timeLeft)
    {
        // Format the time without decimals
        int timeWithoutDecimals = Mathf.FloorToInt(timeLeft);
        timeLefText.text = "Time left: " + timeWithoutDecimals.ToString();
    }
}
