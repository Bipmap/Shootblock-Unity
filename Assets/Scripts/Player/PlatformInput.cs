using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformInput : MonoBehaviour {

    float timer = 0;
    bool touching = false;
    bool began = false;

    public enum InputState
    {
        none,
        beginShoot,
        shooting,
        jumping
    }
    public InputState input = InputState.none;

    // Update is called once per frame
    void Update () {
        //Standalone shooting processing
#if UNITY_STANDALONE
        if (Input.GetKey(KeyCode.Mouse0)) input = InputState.shooting;
        if (Input.GetKeyDown(KeyCode.Mouse0)) input = InputState.beginShoot;
        if (!Input.GetKey(KeyCode.Mouse0) && !Input.GetKeyDown(KeyCode.Mouse0)) input = InputState.none;
#endif

        //Android shoot/jump processing
#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            if ((Input.GetTouch(0).phase == TouchPhase.Began) && !touching)
            {
                touching = true;
                began = true;
            }
            else if (((Input.GetTouch(0).phase == TouchPhase.Moved) || (Input.GetTouch(0).phase == TouchPhase.Stationary)) && !touching)
            {
                touching = true;
                began = false;
            }
        }
        else input = InputState.none;

        if (touching == true) timer += Time.deltaTime;
        if (timer >= 0.06f)
        {
            switch (Input.touchCount)
            {
                case 0: input = InputState.none;
                break;

                case 1:
                {
                    if (began == true) input = InputState.beginShoot;
                    else if (began == false) input = InputState.shooting;
                }
                break;

                case 2: input = InputState.jumping;
                break;
            }
            timer = 0;
            touching = false;
        }
#endif
    }
}
