using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSaveController : MonoBehaviour
{
    public static int PuzzleBestScore = PlayerPrefs.GetInt("PuzzleBestScore");


    private void Start()
    {

    }

    public static void ScoreUpdate()
    {

        if (PuzzleController.CurretScore >= PuzzleBestScore)
        {
            PuzzleController.CurretScore = PuzzleBestScore;
            PuzzleController.BestScore.text = "New BestScore!";
        }
       
    }
}
