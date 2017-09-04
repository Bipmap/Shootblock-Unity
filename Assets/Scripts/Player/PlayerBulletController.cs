using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBulletController : MonoBehaviour
{

    //Initialize variables
    Rigidbody2D rb2d;
    int maskF;
    int maskT;
    int maskE;
    int maskI;
    bool killNow = false;
    RaycastHit2D hitF;
    RaycastHit2D hitT;
    RaycastHit2D hitE;
    RaycastHit2D hitI;
    int framecounter = 0;

    // Use this for initialization
    void Start()
    {
        //Get components and masks
        rb2d = GetComponent<Rigidbody2D>();
        maskF = LayerMask.GetMask("Floor");
        maskT = LayerMask.GetMask("Target");
        maskE = LayerMask.GetMask("Enemy");
        maskI = LayerMask.GetMask("InactiveEnemy");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Kill loop, one frame late
        if (killNow)
        {
            Destroy(gameObject);
            if (hitT.collider != null)
            {
                hitT.collider.GetComponent<ShootingTargetController>().Kill();
            }
            if (hitE.collider != null)
            {
                hitE.collider.GetComponent<EnemyController>().Kill();
            }
            if (hitI.collider != null)
            {
                hitI.collider.GetComponent<InactiveEnemy>().Kill();
            }
        }

        //Run RacyCheck
        RayCheck(hitF, maskF);
        hitT = RayCheck(hitT, maskT);
        hitE = RayCheck(hitE, maskE);
        hitI = RayCheck(hitI, maskI);

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
            result = Physics2D.Raycast(transform.position, -rb2d.velocity, rb2d.velocity.magnitude * Time.deltaTime, mask);
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
