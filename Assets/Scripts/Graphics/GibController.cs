using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GibController : MonoBehaviour {

    //Initialize variables
    SpriteRenderer sprite;
    Rigidbody2D rb2d;
    Sprite[] side = new Sprite[3];
    int variation;
    float spin;
    int killCounter = 200;

	// Use this for initialization
	void Start () {
        //Load resources
        for (int i = 1; i <= 3; i++)
        {
            side[i - 1] = Resources.Load<Sprite>(@"Sprites\side" + i);
        }
        //Get components
        sprite = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        //Set random factors
        variation = Random.Range(0, side.Length);
        spin = Random.Range(-100f, 100f);
        //Assign sprite
        sprite.sprite = side[variation];
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        rb2d.rotation += spin;
        killCounter--;
        if (killCounter == 0)
        {
            Destroy(gameObject);
        }
	}
}
