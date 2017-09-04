using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    //Initialize variables
    GameObject player;

    void Start()
    {
        //Add self to dictionary and set inactive
        RuntimeDictionary.RuntimeObjects.Add("Respawn Button", gameObject);
        gameObject.SetActive(false);
    }

    public void Respawn()
    {
        //If respawn is triggered, reset the level
        RuntimeDictionary.RuntimeObjects.Clear();
        SceneManager.LoadScene("Level 1");
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
