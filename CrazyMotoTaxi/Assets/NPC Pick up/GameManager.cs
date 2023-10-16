using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEditor.PackageManager;

public class GameManager : SinglentonParent<GameManager>
{
    [SerializeField]
    private float score = 0f;

    private float actualPayment;
    private float timeLeft = 90f;
    private float passengerTime = 15f;
    private Coroutine taxiFareCoroutine;

    public delegate void GameOverAction();
    public delegate void ScoreUpdatedAction(float score);
    public delegate void TimeLeftUpdatedAction(float timeLeft);
    public delegate void ActualFareUpdatedAction(float actualFare);

    public event GameOverAction OnGameOver;
    public event ScoreUpdatedAction OnScoreUpdated;
    public event TimeLeftUpdatedAction OnTimeLeftUpdated;
    public event ActualFareUpdatedAction OnActualFareUpdated;

    private void Update()
    {
        timeLeft -= Time.deltaTime;

        // Check for game over condition based on timeLeft
        if (timeLeft <= 0)
        {
            OnGameOver.Invoke(); // Trigger the game over event
        }
        OnTimeLeftUpdated?.Invoke(timeLeft);
    }

    public void Score(float fare)
    {
        score += fare;
        passengerTime = 15f;
        // TODO UI
        OnScoreUpdated?.Invoke(score);

    }


    // Start the taxi fare calculation coroutine
    public void StartTaxiFare(float maxPayment)
    {
        if (taxiFareCoroutine == null)
        {
            taxiFareCoroutine = StartCoroutine(CalculateTaxiFare(maxPayment));
        }
    }

    // Stop the taxi fare calculation coroutine
    public void StopTaxiFare()
    {
        if (taxiFareCoroutine != null)
        {
            StopCoroutine(taxiFareCoroutine);
            taxiFareCoroutine = null;
            
        }
        Score(actualPayment);
        OnActualFareUpdated?.Invoke(0f);
    }

    // Coroutine to calculate the taxi fare over time
    private IEnumerator CalculateTaxiFare(float maxPayment)
    {
        actualPayment = maxPayment;

        // Calculate the initial payment rate based on passenger time
        float initialPaymentRate = 1.0f; // Full payment if passenger time is 15 seconds
        float minPaymentRate = 0.2f; // Minimum payment rate (20% of original value)
        float paymentRate = initialPaymentRate;

        while (actualPayment > 0 && passengerTime > 0)
        {
            // Calculate the payment rate based on remaining passenger time
            float remainingTimePercentage = passengerTime / 15.0f; // 15 seconds is the full passenger time
            paymentRate = Mathf.Lerp(initialPaymentRate, minPaymentRate, 1.0f - remainingTimePercentage);

            // Decrease payment over time based on the payment rate
            actualPayment = maxPayment * paymentRate;

            OnActualFareUpdated?.Invoke(actualPayment);

            // Decrease passenger time
            passengerTime -= Time.deltaTime;

            // Update the UI or perform other actions as needed to display the current payment and passenger time

            yield return null;
        }
    }
}