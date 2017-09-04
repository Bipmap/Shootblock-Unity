using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject {

    //Initialize variables
    float maxSpeed = 4;
    float jumpSpeed = 6;

    float accelSpeed = 0.6f;
    float decaySpeed = 0.8f;

    Vector2 move = Vector2.zero;

#if UNITY_ANDROID
    PlatformInput touchInput;
    float maxTilt;
    float deadzoneTilt;
    float lerpAngle;

    protected override void OnStart()
    {
        //Get input handler
        touchInput = GameObject.Find("Input Handler").GetComponent<PlatformInput>();
        maxTilt = Settings.settings[1];
        deadzoneTilt = Settings.settings[3];
    }
#endif

    //Input processing
    protected override void ComputeVelocity()
    {
#if UNITY_STANDALONE
        //Get horizontal input
        move.x = System.Convert.ToInt32(Input.GetKey(KeyCode.D)) - System.Convert.ToInt32(Input.GetKey(KeyCode.A));

        //Get jump input
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && grounded)
        {
            velocity.y = jumpSpeed;
        }
        //Jump holding
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.Space))
        {
            if (velocity.y > 0)
            {
                velocity = new Vector2(velocity.x, velocity.y * .5f);
            }
        }

        //Add horizontal movement
        velocity.x += move.x * accelSpeed;
        //Turn around sharply
        if (move.x == 0) velocity.x *= decaySpeed;
        if (velocity.x > 0 && move.x < 0) velocity.x = 0;
        if (velocity.x < 0 && move.x > 0) velocity.x = 0;
        //Clamp velocity
        velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);

#elif UNITY_ANDROID
        //Get horizontal input
        float rawAngle = Input.acceleration.x;
        if (rawAngle >= 0) lerpAngle = Mathf.InverseLerp(deadzoneTilt, maxTilt, rawAngle);
        else if (rawAngle < 0) lerpAngle = -Mathf.InverseLerp(deadzoneTilt, maxTilt, -rawAngle);
        move.x = lerpAngle;

        //Get jump input
        if (touchInput.input == PlatformInput.InputState.jumping && grounded)
        {
            velocity.y = jumpSpeed;
        }
        //Jump holding
        else if (touchInput.input != PlatformInput.InputState.jumping)
        {
            if (velocity.y > 0)
            {
                velocity = new Vector2(velocity.x, velocity.y * .5f);
            }
        }

        velocity.x = move.x * 4;
#endif
    }

    //Rotation
    protected override void ComputeRotation()
    {
        //Add rotation
        if (!grounded && Mathf.Abs(rb2d.velocity.x) > 0)
        {
            rb2d.rotation -= rb2d.velocity.x * 2.6f;
        }
        //Stop rotation
        else if (grounded || rb2d.velocity.x == 0)
        {
            rb2d.rotation = 0;
        }
    }
}
