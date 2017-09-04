using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    //Initialize variables
    CircleCollider2D cc2d;
    GameObject player;
    PlayerGunController script;
    GameObject gun;
    BoxSpawnController controller;
    PickupText text;
    AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        //Get components
        cc2d = GetComponent<CircleCollider2D>();
        controller = GameObject.Find("Box Spawn Controller").GetComponent<BoxSpawnController>();
        text = GameObject.Find("Pickup Text").GetComponent<PickupText>();
        audioSource = GameObject.Find("Box Spawn Controller").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Check if player exists
        if (RuntimeDictionary.RuntimeObjects.ContainsKey("Player"))
        {
            //Assign player and gun GameObjects
            if (player == null)
            {
                RuntimeDictionary.RuntimeObjects.TryGetValue("Player", out player);
                RuntimeDictionary.RuntimeObjects.TryGetValue("Gun", out gun);
                script = gun.GetComponent<PlayerGunController>();
            }
            //If collide with player, randomly pick a gun to give
            if (cc2d.IsTouching(player.GetComponent<CircleCollider2D>()))
            {
                int weapon = Random.Range(0, 5);
                while (weapon == controller.lastBox) weapon = Random.Range(0, 5);
                audioSource.Play();
                switch (weapon)
                {
                    case 0:
                        script.currentGun = PlayerGunController.Gun.shotgun;
                        controller.lastBox = 0;
                        text.DisplayPickupText("Shotgun");
                        break;

                    case 1:
                        script.currentGun = PlayerGunController.Gun.smg;
                        text.DisplayPickupText("SMG");
                        controller.lastBox = 1;
                        break;

                    case 2:
                        script.currentGun = PlayerGunController.Gun.zooka;
                        text.DisplayPickupText("Boomzooka");
                        controller.lastBox = 2;
                        break;

                    case 3:
                        script.currentGun = PlayerGunController.Gun.revolver;
                        text.DisplayPickupText("Revolver");
                        controller.lastBox = 3;
                        break;

                    case 4:
                        script.currentGun = PlayerGunController.Gun.spingun;
                        text.DisplayPickupText("Spingun");
                        controller.lastBox = 4;
                        break;

                    default: break;
                }
                //Initialize the gun
                script.initialize = true;
                //Kill the box
                Destroy(gameObject);
            }
        }
    }
}
