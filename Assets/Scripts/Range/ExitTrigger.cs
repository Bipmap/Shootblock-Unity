using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTrigger : MonoBehaviour {

    //Go to Menu on collide
    void OnCollisionEnter2D(Collision2D collision)
    {
        RuntimeDictionary.RuntimeObjects.Clear();
        if (collision.gameObject.name == "Player(Clone)") SceneManager.LoadScene("Menu");
    }
}
