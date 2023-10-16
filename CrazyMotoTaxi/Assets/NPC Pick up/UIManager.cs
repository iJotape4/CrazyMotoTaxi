using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI timeLefText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI currentFareText;

    private void OnEnable()
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.OnScoreUpdated += UpdateScore;
            GameManager.Instance.OnTimeLeftUpdated += UpdateTimeLeft;
            GameManager.Instance.OnActualFareUpdated += UpdateCurrentFare;
        }
        else
        {
            print("lol");
        }
   
    }

    private void UpdateScore(float score)
    {
        // Round the score to the nearest 50 units
        int roundedScore = Mathf.RoundToInt(score / 50) * 50;
        scoreText.text = "Cash: " + roundedScore + "$";
    }

    private void UpdateTimeLeft(float timeLeft)
    {
        // Format the time without decimals
        int timeWithoutDecimals = Mathf.FloorToInt(timeLeft);
        timeLefText.text = "Time left: " + timeWithoutDecimals.ToString();
    }

    private void UpdateCurrentFare(float currentFare)
    {
        // Round the currentFare to the nearest 50 units
        int roundedFare = Mathf.RoundToInt(currentFare / 50) * 50;
        currentFareText.text = roundedFare + "$";
    }
}
