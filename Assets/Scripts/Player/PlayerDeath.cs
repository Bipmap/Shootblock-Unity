using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    //Kill the player
    public static void Die()
    {
        //Unlock mouse
#if UNITY_STANDALONE
        Cursor.lockState = CursorLockMode.None;
#endif
        //Enable respawn button
        GameObject reset;
        RuntimeDictionary.RuntimeObjects.TryGetValue("Respawn Button", out reset);
        reset.SetActive(true);
        //Clear RuntimeDictionary
        RuntimeDictionary.RuntimeObjects.Clear();
        //End combo
        GameObject combo = GameObject.Find("Combo Counter");
        combo.GetComponent<ComboController>().AddToScore();
    }
}
