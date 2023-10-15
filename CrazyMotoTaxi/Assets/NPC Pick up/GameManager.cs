using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameManager : SinglentonParent<GameManager>
{

    [SerializeField]
    private int score = 0;

    public void Score()
    {
        score++;
    }
}