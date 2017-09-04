using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    //Initialize variables
    protected Rigidbody2D rb2d;
    protected CircleCollider2D cc2d;
    protected Vector2 velocity;
    protected int mask;
    protected int maskPlat;
    float gravityModifier = 1.4f;
    protected bool grounded = false;

    void OnEnable()
    {
        //Get components
        rb2d = GetComponent<Rigidbody2D>();
        cc2d = GetComponent<CircleCollider2D>();
    }

    // Use this for initialization
    void Start()
    {
        //Assign mask ints
        mask = LayerMask.GetMask("Floor");
        maskPlat = LayerMask.GetMask("Platform");
        //Custom start
        OnStart();
    }

    // Update is called once per frame
    void Update()
    {
        ComputeVelocity();
    }

    //Physics calculation
    void FixedUpdate()
    {
        //Add gravity
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;

        //Unground
        grounded = false;

        //Check downward collision
        //Raycast downward with distance buffer based on mask size
        RaycastHit2D hitD = Physics2D.Raycast(rb2d.position, Vector2.down, cc2d.radius - (Time.deltaTime * velocity.y), mask);
        //Check if collided
        if (hitD.collider != null)
        {
            grounded = true;
            //Set position to the collided object
            rb2d.position = new Vector2(rb2d.position.x, hitD.point.y + cc2d.radius);
            //Set appropritate velocity to 0
            velocity = new Vector2(velocity.x, 0f);
        }

        //Check rightward collision
        RaycastHit2D hitR = Physics2D.Raycast(rb2d.position, Vector2.right, cc2d.radius + (Time.deltaTime * velocity.x), mask);
        if (hitR.collider != null)
        {
            rb2d.position = new Vector2(hitR.point.x - cc2d.radius, rb2d.position.y);
            velocity = new Vector2(0f, velocity.y);
        }

        //Check leftward collision
        RaycastHit2D hitL = Physics2D.Raycast(rb2d.position, Vector2.left, cc2d.radius - (Time.deltaTime * velocity.x), mask);
        if (hitL.collider != null)
        {
            rb2d.position = new Vector2(hitL.point.x + cc2d.radius, rb2d.position.y);
            velocity = new Vector2(0f, velocity.y);
        }

        //Check upward collision
        RaycastHit2D hitU = Physics2D.Raycast(rb2d.position, Vector2.up, cc2d.radius + (Time.deltaTime * velocity.y), mask);
        if (hitU.collider != null)
        {
            rb2d.position = new Vector2(rb2d.position.x, hitU.point.y - cc2d.radius);
            velocity = new Vector2(velocity.x, 0f);
        }

        //Check velocity-based collision
        RaycastHit2D hitM = Physics2D.Raycast(rb2d.position, velocity, cc2d.radius + (Time.deltaTime * velocity.magnitude), mask);
        if (hitM.collider != null)
        {
            rb2d.position = hitM.point - (rb2d.velocity * Time.deltaTime * cc2d.radius);
            velocity = new Vector2(0f, 0f);
        }

        //Check platform collision
        RaycastHit2D hitP = Physics2D.Raycast(rb2d.position, Vector2.down, cc2d.radius - (Time.deltaTime * velocity.y), maskPlat);
        if (hitP.collider != null)
        {
            grounded = true;
            rb2d.position = new Vector2(rb2d.position.x, hitP.point.y + cc2d.radius);
            velocity = new Vector2(velocity.x, 0f);
        }

        //Set rb2d velocity
        rb2d.velocity = velocity;
        ComputeRotation();
    }

    //Input
    protected virtual void ComputeVelocity()
    {

    }

    //Spinny
    protected virtual void ComputeRotation()
    {

    }

    //Starting stuff
    protected virtual void OnStart()
    {

    }
}
