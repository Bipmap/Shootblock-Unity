using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour {

    //Initialize variables
    int maskP;
    int maskE;

    void Start()
    {
        maskE = LayerMask.NameToLayer("Enemy");
        maskP = LayerMask.NameToLayer("Player");
    }

    //Check when colliding
    void OnCollisionEnter2D(Collision2D collision)
    {
        //If player, run death script
        if (collision.gameObject.layer == maskP)
        {
            Destroy(collision.transform.parent.gameObject);
            PlayerDeath.Die();
        }
        //If enemy, run death script
        if (collision.gameObject.layer == maskE)
        {
            EnemyController script = collision.gameObject.GetComponent<EnemyController>();
            script.Kill();
        }
    }
}
