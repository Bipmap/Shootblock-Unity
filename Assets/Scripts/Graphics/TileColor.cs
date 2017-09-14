using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileColor : MonoBehaviour {

    //Initialize variables
    SpriteRenderer sprite;
    Color col;
    Color[] combo = new Color[11];
    int currentCol = 0;
    float increment = 0f;
    bool increasing = false;
    bool resetting = false;
    ComboController comboScript;

	// Use this for initialization
	void Start () {
        //Declare colors
        combo[0] = new Color(0.239f, 0.506f, 0.588f, 1);
        combo[1] = new Color(0.196f, 0.353f, 0.369f, 1);
        combo[2] = new Color(0.396f, 0.122f, 0.443f, 1);
        combo[3] = Color.red;
        combo[4] = Color.cyan;
        combo[5] = Color.gray;
        combo[6] = Color.green;
        combo[7] = Color.yellow;
        combo[8] = Color.black;
        combo[9] = Color.white;
        combo[10] = Color.clear;
        //Get components
        sprite = GetComponent<SpriteRenderer>();
        //Assign color
        switch (SceneManager.GetActiveScene().name)
        {
            case ("Menu"):
            {
                col = Color.black;
            }
            break;

            case ("Level 1"):
            {
                col = combo[currentCol];
                comboScript = GameObject.Find("Combo Counter").GetComponent<ComboController>();
            }
            break;

            default: col = Color.black;
            break;
        }

        //Assign sprite color
        sprite.color = col;
	}

    void Update ()
    {
        if (SceneManager.GetActiveScene().name != "Menu")
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
        
}
