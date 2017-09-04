using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTargetController : MonoBehaviour {

    public void Kill()
    {
        Destroy(gameObject);
    }
}
