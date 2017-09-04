using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeEnemy : MonoBehaviour
{

    //Initialize variables
    GameObject gun;

    // Use this for initialization
    void Start()
    {
        gun = (GameObject)Instantiate(Resources.Load("Enemy Gun"), gameObject.transform);
    }
}
