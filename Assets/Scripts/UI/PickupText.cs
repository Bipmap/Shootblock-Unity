using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupText : MonoBehaviour {

    //Initialize variables
    public Text text;
    RectTransform pos;
    Color col;
    Vector3 movement;
    Vector3 startPos;
    float transparency;
    bool displaying = false;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        pos = GetComponent<RectTransform>();
        text.enabled = false;
        movement = new Vector3(0, 0.01f, 0);
        startPos = pos.localPosition;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //Check if text is visible
		if (displaying == true)
        {
            //Decrement transparency and change color
            transparency -= 0.05f;
            col.a = transparency;
            text.color = col;
            //Move text
            pos.position += movement;
            //Delete if invisible
            if (transparency <= 0)
            {
                text.enabled = false;
                displaying = false;
            }
        }
	}

    //Reset variables
    public void DisplayPickupText(string name)
    {
        pos.localPosition = startPos;
        text.text = name;
        transparency = 1f;
        col = new Color(1, 1, 1, transparency);
        text.color = col;
        text.enabled = true;
        displaying = true;
    }
}
