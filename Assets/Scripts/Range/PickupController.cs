using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour {

    string gunName;
    BoxCollider2D bc2d;
    SpriteRenderer sprite;
    PlayerGunController script;
    PickupText text;
    bool disabled = false;
    int enableTimer = 60;
    GameObject gun;

	// Use this for initialization
	void Start () {
        gunName = gameObject.name;
        text = GameObject.Find("Pickup Text").GetComponent<PickupText>();
        bc2d = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		if (disabled)
        {
            enableTimer--;
            if (enableTimer == 0)
            {
                bc2d.enabled = true;
                sprite.enabled = true;
                disabled = false;
            }
        }
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player(Clone)")
        {
            RuntimeDictionary.RuntimeObjects.TryGetValue("Gun", out gun);
            script = gun.GetComponent<PlayerGunController>();
            if (gunName == "Shotgun") script.currentGun = PlayerGunController.Gun.shotgun;
            if (gunName == "SMG") script.currentGun = PlayerGunController.Gun.smg;
            if (gunName == "Boomzooka") script.currentGun = PlayerGunController.Gun.zooka;
            if (gunName == "Revolver") script.currentGun = PlayerGunController.Gun.revolver;
            if (gunName == "Spingun") script.currentGun = PlayerGunController.Gun.spingun;
            script.initialize = true;
            text.DisplayPickupText(gunName);
            bc2d.enabled = false;
            sprite.enabled = false;
            enableTimer = 60;
            disabled = true;
        }
    }
}
