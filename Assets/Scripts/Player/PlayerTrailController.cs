using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrailController : MonoBehaviour {

    TrailRenderer trail;
    ComboController comboScript;

	// Use this for initialization
	void Start () {
        trail = GetComponent<TrailRenderer>();
        comboScript = GameObject.Find("Combo Counter").GetComponent<ComboController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (comboScript.combo >= 30) trail.enabled = true;
        else trail.enabled = false;
	}
}
