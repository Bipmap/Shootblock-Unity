using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileColor : MonoBehaviour {

    //Initialize variables
    SpriteRenderer sprite;
    Color col;
    Color level1;

	// Use this for initialization
	void Start () {
        //Declare colors
        level1 = new Color(0.239f, 0.506f, 0.588f, 1);
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
                col = level1;
            }
            break;

            default: col = Color.black;
            break;
        }

        //Assign sprite color
        sprite.color = col;
	}
}
