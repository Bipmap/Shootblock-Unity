using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboScalarToggle : MonoBehaviour {

    Text text;
    public bool on = true;

    void Start()
    {
        text = GetComponent<Text>();
    }

    public void Toggle()
    {
        on = !on;
        if (on) text.text = "on";
        else if (!on) text.text = "off";
    }

    public void SetOn()
    {
        on = true;
        text.text = "on";
    }
}
