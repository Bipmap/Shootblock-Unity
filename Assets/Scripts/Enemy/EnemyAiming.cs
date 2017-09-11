using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiming : MonoBehaviour
{

    //Initialize variables
    float aimDistance = 1.6f;
    public int countdown;
    public int countdownScalar;
    int aimTimer;
    int mask;
    GameObject player;
    GameObject gun;
    GameObject circle;
    Vector2 bulletSpeed;
    Vector3 disp;
    SpriteRenderer aimCircle;
    SpriteRenderer rend;
    EnemyController script;
    public bool fire = false;
    AudioSource source;
    LineRenderer line;

    void Start()
    {
        //Set displacement and speed
        disp = new Vector3(0.4f, 0, 0);
        bulletSpeed = new Vector2(3.5f, 0);
        //Reference parent script
        script = transform.parent.GetComponent<EnemyController>();
        //Find mask for vision
        mask = LayerMask.GetMask("Floor");
        //Find renderer for flipping
        rend = GetComponent<SpriteRenderer>();
        //Find LineRenderer for visual countdown
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        //Add AudioSource
        source = gameObject.AddComponent<AudioSource>();
        //Set position for first frame
        float chaseAngle = 0;
        if (Mathf.Sign(transform.parent.GetComponent<Rigidbody2D>().velocity.x) == 1) chaseAngle = 0;
        else chaseAngle = 180;
        transform.position = transform.parent.transform.position + Mathf.Sign(transform.parent.GetComponent<Rigidbody2D>().velocity.x) * disp;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, chaseAngle));
        //Load and set countdown
        countdownScalar = (int)GameObject.Find("Enemy Spawner").GetComponent<EnemySpawnController>().aimTimer;
        countdown = (int)(75 * StaticSettings.enemyAimSpeed - countdownScalar);
    }

    void FixedUpdate()
    {
        //Assign target
        //player = script.player;

        //State machine
        switch (script.state)
        {
            case EnemyController.eStates.chase:
                {
                    //Aim gun based on movement
                    float chaseAngle = 0;
                    if (Mathf.Sign(transform.parent.GetComponent<Rigidbody2D>().velocity.x) == 1) chaseAngle = 0;
                    else chaseAngle = 180;

                    //Apply gun displacement
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, chaseAngle));
                    transform.position = transform.parent.transform.position + Mathf.Sign(transform.parent.GetComponent<Rigidbody2D>().velocity.x) * disp;

                    //Raycast toward player
                    ComputeVisibility();
                    break;
                }

            case EnemyController.eStates.aim:
                {
                    if (RuntimeDictionary.RuntimeObjects.TryGetValue("Player", out player))
                    {
                        //Find angle to player
                        float aimx = player.transform.position.x - transform.parent.transform.position.x;
                        float aimy = player.transform.position.y - transform.parent.transform.position.y;
                        float angle = Mathf.Atan2(aimy, aimx) * Mathf.Rad2Deg;
                        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

                        //Set displacement and angle
                        transform.position = transform.parent.transform.position + transform.rotation * disp;

                        //Render circle
                        line.enabled = true;
                        float radius = (float)aimTimer / 100;
                        float thickness = 0.03f;
                        int segments = 42;
                        Draw.DrawEllipse(radius, radius, segments, thickness, line);

                        //Decrease timer
                        aimTimer--;

                        if (aimTimer == 0)
                        {
                            //Shoot, reset speed and state
                            fire = true;
                            line.enabled = false;
                            script.maxSpeed = 1;
                            script.state = EnemyController.eStates.chase;
                        }
                    }
                    //If player dies, stop aiming
                    else if (!RuntimeDictionary.RuntimeObjects.TryGetValue("Player", out player))
                    {
                        line.enabled = false;
                        script.state = EnemyController.eStates.chase;
                        script.maxSpeed = 1;
                    }

                    break;
                }

            default: break;
        }
        //Flip sprite
        if ((transform.eulerAngles.z > 90) && (transform.eulerAngles.z < 270))
        {
            rend.flipY = true;
        }
        else rend.flipY = false;

        if (fire)
        {
            //Force aiming on shooting frame
            float aimx = player.transform.position.x - transform.parent.transform.position.x;
            float aimy = player.transform.position.y - transform.parent.transform.position.y;
            float angle = Mathf.Atan2(aimy, aimx) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.position = transform.parent.transform.position + transform.rotation * disp;

            //Create bullet and add velocity
            GameObject bulletInstance = (GameObject)Instantiate(Resources.Load("Enemy Bullet"), gameObject.transform.position, gameObject.transform.rotation);
            Rigidbody2D bulletrb2d = bulletInstance.GetComponent<Rigidbody2D>();
            bulletrb2d.velocity = bulletInstance.transform.rotation * bulletSpeed;

            //Play shoot sound
            source.PlayOneShot((AudioClip)Resources.Load("Sounds/enemyshoot1"));

            fire = false;
        }
    }

    void ComputeVisibility()
    {
        //Assign player
        if (RuntimeDictionary.RuntimeObjects.TryGetValue("Player", out player))
        {
            //Raycast to player, finding floors
            RaycastHit2D hitP = Physics2D.Linecast(transform.position, player.transform.position, mask);
            //If line of sight and within radius, return true
            if (hitP.collider == null & Vector2.Distance(transform.position, player.transform.position) < aimDistance)
            {
                //Set timer
                aimTimer = countdown;
                //Switch state
                script.state = EnemyController.eStates.aim;
            }
        }
    }
}
