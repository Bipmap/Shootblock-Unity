using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColor : MonoBehaviour {

    Color col;
    Color[] combo = new Color[11];
    SpriteRenderer sprite;
    ComboController comboScript;
    int currentCol = 0;
    float increment = 0f;
    bool increasing = false;
    bool resetting = false;

    // Use this for initialization
    void Start()
    {
        //Declare colors
        combo[0] = new Color(0.267f, 0.588f, 0.859f, 1);
        combo[1] = new Color(0.431f, 0.718f, 0.486f, 1);
        combo[2] = new Color(0.616f, 0.639f, 0.643f, 1);
        combo[3] = new Color(0.396f, 0.122f, 0.443f, 1);
        combo[4] = new Color(0.918f, 0.216f, 0.533f, 1);
        combo[5] = new Color(1, 0.953f, 0.569f, 1);
        combo[6] = new Color(0.561f, 0.769f, 0.729f, 1);
        combo[7] = new Color(0.969f, 0.925f, 0.812f, 1);
        combo[8] = new Color(0.204f, 0.204f, 0.29f, 1);
        combo[9] = Color.white;
        combo[10] = Color.clear;
        //Get components
        sprite = GetComponent<SpriteRenderer>();
        comboScript = GameObject.Find("Combo Counter").GetComponent<ComboController>();
        //Assign color
        col = combo[0];
        //Set image color
        sprite.color = col;
    }

    // Update is called once per frame
    void Update()
    {
        if ((comboScript.reset == true) || (comboScript.smallReset == true)) resetting = true;
        if ((comboScript.tens == true) && (comboScript.combo <= 100)) increasing = true;

        if (increasing == true)
        {
            increment += 0.01f;
            col = Color.Lerp(combo[currentCol], combo[currentCol + 1], increment);
            sprite.color = col;
            if (increment >= 1f)
            {
                currentCol += 1;
                col = combo[currentCol];
                sprite.color = col;
                increment = 0f;
                increasing = false;
            }
        }

        if (resetting == true)
        {
            increment += 0.01f;
            col = Color.Lerp(combo[currentCol], combo[0], increment);
            sprite.color = col;
            if (increment >= 1f)
            {
                currentCol = 0;
                col = combo[currentCol];
                sprite.color = col;
                increment = 0f;
                resetting = false;
            }
        }
    }
}
