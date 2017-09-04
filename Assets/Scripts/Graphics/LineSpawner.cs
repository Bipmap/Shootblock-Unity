using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSpawner : MonoBehaviour {

    //Initialize variables
    GameObject[] lines;
    GameObject line;
    int timer = 5;
    int maxLines = 30;

	// Use this for initialization
	void Start () {
        //Load resources
        line = (GameObject)Resources.Load("Line");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //Create array of lines
        lines = GameObject.FindGameObjectsWithTag("Line");
        //Create line if fewer than 10 exist and timer is zero
        if (lines.Length < maxLines && timer == 0)
        {
            Instantiate(line, transform);
            timer = Random.Range(5, 20);
        }
        //Reset timer
        else if (timer == 0)
        {
            timer = Random.Range(3, 10);
        }
        //Decrement timer
        timer--;
    }
}
