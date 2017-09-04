using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletCounter : MonoBehaviour
{

    string objName;
    int num;
    PlayerGunController script;
    Image image;
    GameObject gun;

    // Use this for initialization
    void Start()
    {
        objName = gameObject.name;
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (script == null && RuntimeDictionary.RuntimeObjects.ContainsKey("Gun"))
        {
            RuntimeDictionary.RuntimeObjects.TryGetValue("Gun", out gun);
            script = gun.GetComponent<PlayerGunController>();
        }
        if (RuntimeDictionary.RuntimeObjects.ContainsKey("Gun"))
        {
            num = int.Parse(objName);
            if (script.currentAmmo < num) image.color = new Color(1, 1, 1, 0.4f);
            else image.color = Color.white;
        }
    }
}
