using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyPresets : MonoBehaviour {

    Slider enemyAimSpeed;
    Slider enemyAimScalar;
    Slider comboTimerSpeed;
    ComboScalarToggle comboTimerScalar;

	// Use this for initialization
	void Start () {
        enemyAimSpeed = transform.Find("Enemy Aim Speed Slider").GetComponent<Slider>();
        enemyAimScalar = transform.Find("Enemy Aim Speed Scalar").GetComponent<Slider>();
        comboTimerSpeed = transform.Find("Combo Timer Speed").GetComponent<Slider>();
        comboTimerScalar = transform.Find("Combo Timer Scalar").transform.Find("Toggle").GetComponent<ComboScalarToggle>();
    }
	
	public void OnAdvancedPress()
    {
        enemyAimSpeed.value = 1f;
        enemyAimScalar.value = 1f;
        comboTimerSpeed.value = 1f;
        comboTimerScalar.SetOn();
    }

    public void OnBeginnerPress()
    {
        enemyAimSpeed.value = 1.5f;
        enemyAimScalar.value = 1f;
        comboTimerSpeed.value = 1.5f;
        comboTimerScalar.SetOn();
    }
}
