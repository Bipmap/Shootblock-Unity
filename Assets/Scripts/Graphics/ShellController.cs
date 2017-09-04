using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : PhysicsObject {

    //Initialize variables
    Vector2 startVelocity;
    float spin;
    float decay = 0.98f;
    bool killNow = false;

    protected override void OnStart()
    {
        //Randomize variables
        startVelocity = new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 4.5f));
        spin = Random.Range(-50, 50);
        //Set starting velocity
        velocity = startVelocity;
    }

    protected override void ComputeVelocity()
    {
        //Kill if grounded
        if (killNow)
        {
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(GetComponent<CircleCollider2D>());
            Destroy(this);
        }
        //Decay velocity
        if (!grounded) velocity *= decay;
        else if (grounded)
        {
            velocity = Vector2.zero;
            killNow = true;
        }
    }

    protected override void ComputeRotation()
    {
        //Add spin
        if (!grounded)
        {
            rb2d.rotation += spin;
        }
    }
}
