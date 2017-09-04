using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineController : MonoBehaviour {

    //Initialize variables
    float length;
    float width;
    float offset;
    float speed;
    int color;
    int killTimer = 0;
    RectTransform rtransform;
    Image imageRenderer;
    CameraController cam;
    Color[] colors = new Color[4];
    float alpha = 0.3f;

	// Use this for initialization
	void Start () {
        //Create colors
        colors[0] = new Color(0.702f, 0.635f, 0.91f, alpha);
        colors[1] = new Color(0.541f, 0.49f, 0.82f, alpha);
        colors[2] = new Color(0.584f, 0.537f, 0.898f, alpha);
        colors[3] = new Color(0.553f, 0.49f, 0.949f, alpha);
        //Get components
        rtransform = GetComponent<RectTransform>();
        imageRenderer = GetComponent<Image>();
        cam = GameObject.Find("Main Camera").GetComponent<CameraController>();
        //Randomize length, speed, and color
        offset = Random.Range(-640f, 640f);
        length = Random.Range(400f, 600f);
        speed = Random.Range(5f, 15f);
        color = Random.Range(0, 4);
        //Set color
        imageRenderer.color = colors[color];
        //Set y and x position
        transform.localPosition = new Vector3(offset, 700, 0);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //Set length based on randomization, width based on camera shake
        rtransform.sizeDelta = new Vector2(1 + (cam.intensity * 100), length);
        //Move line downward
        transform.localPosition += new Vector3(0, -speed, 0);
        //Increment and kill
        killTimer++;
        if (killTimer == 320)
        {
            Destroy(gameObject);
        }
	}
}
