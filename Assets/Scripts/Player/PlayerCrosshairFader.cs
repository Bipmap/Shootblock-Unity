using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrosshairFader : MonoBehaviour
{
    //Initialize variables
    SpriteRenderer sprite;
    Color col = Color.white;
    float transparency = 1f;
    GameObject player;

    // Use this for initialization
    void Start()
    {
        //Get components
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //If player exists, fade out then disable
        if (RuntimeDictionary.RuntimeObjects.ContainsKey("Player"))
        {
            transparency -= 0.05f;
            col = new Color(1, 1, 1, transparency);
            sprite.color = col;

            if (transparency <= 0) sprite.enabled = false;
        }
        //If player dies, show back up
        else
        {
            sprite.enabled = true;
            transparency = 1;
            col = new Color(1, 1, 1, transparency);
            sprite.color = col;
        }
    }
}
