using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdate : MonoBehaviour
{

    //Initialize variables
    Text scoreText;
    public int scoreValue = 0;

    // Use this for initialization
    void Start()
    {
        scoreText = GetComponent<Text>();
    }

    //Redraw text
    public void SetScoreText()
    {
        scoreText.text = "Score: " + scoreValue.ToString();
    }
}
