using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{

    //Initialize variables
    Rigidbody2D rb2d;
    int maskF;
    int maskP;
    bool killNow = false;
    RaycastHit2D hitF;
    RaycastHit2D hitP;
    int framecounter = 0;
    GameObject reset;

    // Use this for initialization
    void Start()
    {
        //Get components and masks
        rb2d = GetComponent<Rigidbody2D>();
        maskF = LayerMask.GetMask("Floor");
        maskP = LayerMask.GetMask("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Kill loop, one frame late
        if (killNow)
        {
            Destroy(gameObject);
            if (hitP.collider != null)
            {
                Destroy(hitP.transform.parent.gameObject);
                PlayerDeath.Die();
            }
        }

        //Run RacyCheck
        RayCheck(hitF, maskF);
        hitP = RayCheck(hitP, maskP);

        //Framecounter kill
        framecounter++;
        if (framecounter == 1000) killNow = true;
    }

    //Raycast from bullet toward mask
    RaycastHit2D RayCheck(RaycastHit2D result, int mask)
    {
        //Cast first-frame ray
        if (framecounter == 0)
        {
            //Cast a ray
            result = Physics2D.Raycast(transform.position, rb2d.velocity, -(4 * rb2d.velocity.magnitude * Time.deltaTime), mask);
            if (result.collider != null)
            {
                //Set killnow to true
                rb2d.position = result.point;
                killNow = true;
            }
            else if (result.collider == null)
            {
                //Cast another ray
                result = Physics2D.Raycast(transform.position, rb2d.velocity, rb2d.velocity.magnitude * Time.deltaTime, mask);
                if (result.collider != null)
                {
                    //Set killnow to true
                    rb2d.position = result.point;
                    killNow = true;
                }
            }
            //Return Raycast
            return result;
        }

        //Cast normal ray
        else
        {
            //Cast a ray
            result = Physics2D.Raycast(transform.position, rb2d.velocity, rb2d.velocity.magnitude * Time.deltaTime, mask);
            if (result.collider != null)
            {
                //Set killnow to true
                rb2d.position = result.point;
                killNow = true;
            }
            //Return Raycast
            return result;
        }
    }
}
