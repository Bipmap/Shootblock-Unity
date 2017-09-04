using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExplosionController : MonoBehaviour {

    //Initialize vairables
    int timer;
    int miniTimer;
    int miniTimer2;
    GameObject miniExplosion;
    GameObject miniInstance;
    GameObject miniInstance2;
    float variance = 0.5f;
    int maskE;
    int maskI;
    int maskT;
    AudioClip sound;
    AudioSource source;

    // Use this for initialization
    void Start () {
        //Set timers
        timer = 60;
        miniTimer = 50;
        miniTimer2 = 45;
        //Get audio source
        source = GetComponent<AudioSource>();
        //Load mini and sound
        miniExplosion = (GameObject)Resources.Load(@"Bullets\Mini Explosion");
        sound = (AudioClip)Resources.Load("Sounds/explosion1");
        //Get enemy masks
        maskE = LayerMask.NameToLayer("Enemy");
        maskI = LayerMask.NameToLayer("InactiveEnemy");
        maskT = LayerMask.NameToLayer("Target");
        //Play sound
        source.PlayOneShot(sound);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //Spawn mini explosions
        if (timer == 58)
        {
            miniInstance = Instantiate(miniExplosion, gameObject.transform);
            miniInstance.transform.localPosition = new Vector3(Random.Range(-variance, variance), Random.Range(-variance, variance), 0);
        }
        if (timer == 55)
        {
            miniInstance2 = Instantiate(miniExplosion, gameObject.transform);
            miniInstance2.transform.localPosition = new Vector3(Random.Range(-variance, variance), Random.Range(-variance, variance), 0);
        }

        //Kill if timers are zero
        if (miniTimer == 0) Destroy(miniInstance);
        if (miniTimer2 == 0) Destroy(miniInstance2);
        if (timer == 0) Destroy(gameObject);

        //Decrement timers
        timer--;
        miniTimer--;
        miniTimer2--;
	}

    //Kill enemies on collision
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == maskE)
        {
            collision.gameObject.GetComponent<EnemyController>().Kill();
        }

        if (collision.gameObject.layer == maskI)
        {
            collision.gameObject.GetComponent<InactiveEnemy>().Kill();
        }

        if (collision.gameObject.layer == maskT)
        {
            collision.gameObject.GetComponent<ShootingTargetController>().Kill();
        }
    }
}
