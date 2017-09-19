using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GradientColor : MonoBehaviour {

    Color col;
    Color[] combo = new Color[11];
    Image image;
    ComboController comboScript;
    int currentCol = 0;
    float increment = 0f;
    bool increasing = false;
    bool resetting = false;

    // Use this for initialization
    void Start () {
        //Declare colors
        combo[0] = new Color(0.133f, 0.318f, 0.478f, 1);
        combo[1] = new Color(0.235f, 0.42f, 0.271f, 1);
        combo[2] = new Color(0.369f, 0.369f, 0.369f, 1);
        combo[3] = new Color(0.259f, 0.086f, 0.286f, 1);
        combo[4] = new Color(0.529f, 0.133f, 0.314f, 1);
        combo[5] = new Color(0.588f, 0.561f, 0.341f, 1);
        combo[6] = new Color(0.302f, 0.42f, 0.396f, 1);
        combo[7] = new Color(0.545f, 0.518f, 0.451f, 1);
        combo[8] = new Color(0.125f, 0.125f, 0.176f, 1);
        combo[9] = new Color(0.604f, 0.604f, 0.604f, 1);
        combo[10] = Color.clear;
        //Get components
        image = GetComponent<Image>();
        comboScript = GameObject.Find("Combo Counter").GetComponent<ComboController>();
        //Assign color
        col = combo[0];
        //Set image color
        image.color = col;
    }
	
	// Update is called once per frame
	void Update () {
        if ((comboScript.reset == true) || (comboScript.smallReset == true)) resetting = true;
        if ((comboScript.tens == true) && (comboScript.combo <= 100)) increasing = true;

        if (increasing == true)
        {
            increment += 0.01f;
            col = Color.Lerp(combo[currentCol], combo[currentCol + 1], increment);
            image.color = col;
            if (increment >= 1f)
            {
                currentCol += 1;
                col = combo[currentCol];
                image.color = col;
                increment = 0f;
                increasing = false;
            }
        }

        if (resetting == true)
        {
            increment += 0.01f;
            col = Color.Lerp(combo[currentCol], combo[0], increment);
            image.color = col;
            if (increment >= 1f)
            {
                currentCol = 0;
                col = combo[currentCol];
                image.color = col;
                increment = 0f;
                resetting = false;
            }
        }
    }
}
