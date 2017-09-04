using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChamberController : MonoBehaviour {

    //Initialize variables
    RectTransform rect;
    PlayerGunController gunScript;
    Image image;

    // Use this for initialization
    void Start () {
        //Get components
        rect = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        image.type = Image.Type.Filled;
        image.fillMethod = Image.FillMethod.Vertical;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //Find gun late
        if ((gunScript == null) && RuntimeDictionary.RuntimeObjects.ContainsKey("Player"))
        {
            gunScript = GameObject.Find("Gun(Clone)").GetComponent<PlayerGunController>();
        }
        //Spin chamber
        if (gunScript.currentGun == PlayerGunController.Gun.spingun)
        {
            rect.Rotate(0, 0, -(100f / gunScript.spinDelay - 10f));
            if (gameObject.name == "Chamber")
            {
                image.fillAmount = (gunScript.currentAmmo * (100f / 45f)) / 100f;
            }
        }
        
	}
}
