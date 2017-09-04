﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboController : MonoBehaviour {

    //Initialize variables
    public int combo;
    public bool reset;
    public bool smallReset;
    int fontSize = 90;
    float timer = 0;
    float maxTime = 180;
    ScoreUpdate score;
    Text comboCounter;
    RectTransform timerBar;
    RectTransform timerBackground;

	// Use this for initialization
	void Start () {
        //Get components and children
        score = GameObject.Find("Score Text").GetComponent<ScoreUpdate>();
        comboCounter = transform.Find("Counter").GetComponent<Text>();
        timerBar = transform.Find("Timer").GetComponent<RectTransform>();
        timerBackground = transform.Find("Timer Background").GetComponent<RectTransform>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //Decrement timer
        timer--;
        timer = Mathf.Max(0, timer);
        //Decrement font size, cap, and apply
        fontSize--;
        fontSize = Mathf.Max(fontSize, 90);
        comboCounter.fontSize = fontSize;
        //Reset if timer hits zero
        if (timer <= 0 && combo > 0) AddToScore();
        //Set combo counter text
        comboCounter.text = "x" + combo;
        //Set timer bar thickness
        float bar = ((-130f) / maxTime) * timer + 100f;
        timerBar.offsetMin = new Vector2(bar, timerBar.offsetMin.y);
        //Hide timer if combo is zero
        if (combo == 0)
        {
            comboCounter.enabled = false;
            timerBar.gameObject.GetComponent<Image>().enabled = false;
            timerBackground.gameObject.GetComponent<Image>().enabled = false;
        }
        else
        {
            comboCounter.enabled = true;
            timerBar.gameObject.GetComponent<Image>().enabled = true;
            timerBackground.gameObject.GetComponent<Image>().enabled = true;
        }
    }

    public void Increment()
    {
        //Add to combo, reset timer based on combo
        combo++;
        maxTime = 180 - combo;
        maxTime = Mathf.Max(100, maxTime);
        timer = maxTime;
        fontSize = 110;
    }

    public void AddToScore()
    {
        //Add to score, reset combo
        score.scoreValue += (int)Mathf.Pow(combo, 2);
        score.SetScoreText();
        if (combo > 20) reset = true;
        else smallReset = true;
        combo = 0;
        maxTime = 180;
    }
}
