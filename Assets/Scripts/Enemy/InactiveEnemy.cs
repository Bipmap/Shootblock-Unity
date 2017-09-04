using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InactiveEnemy : MonoBehaviour
{

    //Initialize variables
    SpriteRenderer sprite;
    float timer = 70;
    ScoreUpdate score;
    ComboController combo;
    bool dying = false;
    GameObject side;

    // Use this for initialization
    void Start()
    {
        //Get component
        sprite = GetComponent<SpriteRenderer>();
        //Set color
        sprite.color = Color.clear;
        //Load resources
        side = (GameObject)Resources.Load("Side");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Run alpha decreasing timer loop
        timer--;
        Color col = new Color(1, 1, 1, 1 - timer / 70);
        sprite.color = col;
        if (timer == 0)
        {
            Destroy(gameObject);
            Instantiate(Resources.Load("Enemy"), gameObject.transform.position, gameObject.transform.rotation);
        }
    }

    //Kill it
    public void Kill()
    {
        //Catch for same-frame collisions
        if (dying == false)
        {
            dying = true;
            //Assign text component and add score
            score = GameObject.Find("Score Text").GetComponent<ScoreUpdate>();
            score.scoreValue += 5;
            score.SetScoreText();
            //Add to combo counter
            combo = GameObject.Find("Combo Counter").GetComponent<ComboController>();
            combo.Increment();
            //Spawn sides
            int n = 0;
            while (n < 4)
            {
                GameObject sideInstance = Instantiate(side, transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 89))));
                Rigidbody2D siderb2d = sideInstance.GetComponent<Rigidbody2D>();
                siderb2d.velocity += new Vector2(Random.Range(-3, 3), Random.Range(2, 8));
                n++;
            }
            //Real kill
            Destroy(gameObject);
        }
    }
}
