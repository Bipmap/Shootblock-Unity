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
    bool ingame = false;

	// Use this for initialization
	void Start () {
        //Declare colors
        combo[0] = new Color(0.239f, 0.506f, 0.588f, 1);
        combo[1] = new Color(0.408f, 0.659f, 0.592f, 1);
        combo[2] = new Color(0.165f, 0.176f, 0.204f, 1);
        combo[3] = new Color(0.114f, 0.047f, 0.125f, 1);
        combo[4] = new Color(0.655f, 0.451f, 0.604f, 1);
        combo[5] = new Color(0.459f, 0.467f, 0.38f, 1);
        combo[6] = new Color(0.675f, 0.867f, 0.906f, 1);
        combo[7] = new Color(0.678f, 0.667f, 0.749f, 1);
        combo[8] = new Color(0.918f, 0.871f, 0.855f, 1);
        combo[9] = new Color(0.208f, 0.231f, 0.235f, 1);
        combo[10] = new Color(0, 0, 0, 0.5f);
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
                ingame = true;
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
        if (ingame == true)
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
