using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInitialization : MonoBehaviour {
    //Initalize varaibles
    GameObject player;
    GameObject gun;
    GameObject crosshair;

    //Create player, gun, and crosshair from resources folder and add to the Runtime Dictionary
    void Start()
    {
        player = (GameObject)Instantiate(Resources.Load("Player"), gameObject.transform);
        RuntimeDictionary.RuntimeObjects.Add("Player", player);
        player.transform.localPosition = new Vector3(0, 0, 0);
        gun = (GameObject)Instantiate(Resources.Load("Gun"), gameObject.transform);
        RuntimeDictionary.RuntimeObjects.Add("Gun", gun);
        crosshair = (GameObject)Instantiate(Resources.Load("Crosshair"), gameObject.transform);
        RuntimeDictionary.RuntimeObjects.Add("Crosshair", crosshair);
    }
}
