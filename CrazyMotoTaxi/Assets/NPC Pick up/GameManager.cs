using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private int score = 0;


    // Static reference to the single instance of GameManager.    
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        // Check if an instance already exists.
        if (Instance == null)
        {
            // If not, set this as the instance.
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this GameObject persistent across scenes.
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Score()
    {
        score++;
    }
}