using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileOnly : MonoBehaviour {
    //Remove Quit button if on Android
#if UNITY_ANDROID
    // Use this for initialization
    void Start () {
        gameObject.SetActive(false);
	}
#endif
}
