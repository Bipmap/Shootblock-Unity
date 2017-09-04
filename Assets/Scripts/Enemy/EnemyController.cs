using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : PhysicsObject
{

    //Initialize variables
    public float maxSpeed = 1;
    public float jumpSpeed = 6f;
    public float accelSpeed = 0.3f;
    public float decaySpeed = 0.8f;
    public float aimDistance = 5;
    public int countdown = 75;
    float dir = 1;
    int aimTimer;
    public GameObject player;
    public bool playerExists = false;
    GameObject gun;
    GameObject circle;
    Vector2 move = Vector2.zero;
    SpriteRenderer aimCircle;
    ScoreUpdate score;
    ComboController combo;
    bool dying = false;

    //Initialize state machine and state
    public enum eStates
    {
        chase,
        aim
    }
    public eStates state = eStates.chase;

    protected override void ComputeVelocity()
    {
        //Find player
        playerExists = RuntimeDictionary.RuntimeObjects.TryGetValue("Player", out player);
        if (playerExists == true)
        {
            switch (state)
            {
                case eStates.chase:
                    {
                        //Move toward player on X axis
                        dir = Mathf.Sign(player.transform.position.x - transform.position.x);
                        move.x = dir;

                        //Raycast to close walls and jump if too close
                        if (Physics2D.Raycast(rb2d.position, move, cc2d.radius + (Time.deltaTime * velocity.x), mask) && grounded)
                        {
                            velocity.y = jumpSpeed;
                        }
                        break;
                    }

                case eStates.aim:
                    {
                        //Slow down while aiming
                        maxSpeed = 0.3f;
                        break;
                    }

                default: break;
            }
        }

        //If no players exist, roam
        else if (playerExists == false)
        {
            move.x = dir;
            maxSpeed = 1;
            if (Physics2D.Raycast(rb2d.position, move, cc2d.radius + (Time.deltaTime * velocity.x), mask))
            {
                dir = -dir;
            }
        }

        //Add decay and such
        velocity.x += move.x * accelSpeed;
        if (move.x == 0) velocity.x *= decaySpeed;
        if (velocity.x > 0 && move.x < 0) velocity.x = 0;
        if (velocity.x < 0 && move.x > 0) velocity.x = 0;
        velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
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
                GameObject sideInstance = (GameObject)Instantiate(Resources.Load("Side"), transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 89))));
                Rigidbody2D siderb2d = sideInstance.GetComponent<Rigidbody2D>();
                siderb2d.velocity += new Vector2(Random.Range(-3, 3), Random.Range(2, 8));
                n++;
            }
            //Real kill
            Destroy(gameObject);
        }
    }
}