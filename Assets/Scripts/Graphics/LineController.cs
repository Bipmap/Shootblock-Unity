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
    Color[] colors = new Color[44];
    float alpha = 0.3f;
    ComboController comboScript;
    float increment = 0f;
    int colorMod = 0;
    bool increasing = false;
    bool resetting = false;
    Color col;

    // Use this for initialization
    void Start () {
        //Create colors
        colors[0] = new Color(0.702f, 0.635f, 0.91f, alpha);
        colors[1] = new Color(0.541f, 0.49f, 0.82f, alpha);
        colors[2] = new Color(0.584f, 0.537f, 0.898f, alpha);
        colors[3] = new Color(0.553f, 0.49f, 0.949f, alpha);

        colors[4] = Color.green;
        colors[5] = Color.green;
        colors[6] = Color.green;
        colors[7] = Color.green;

        colors[8] = Color.blue;
        colors[9] = Color.blue;
        colors[10] = Color.blue;
        colors[11] = Color.blue;

        colors[12] = Color.red;
        colors[13] = Color.red;
        colors[14] = Color.red;
        colors[15] = Color.red;

        colors[16] = Color.cyan;
        colors[17] = Color.cyan;
        colors[18] = Color.cyan;
        colors[19] = Color.cyan;

        colors[20] = Color.yellow;
        colors[21] = Color.yellow;
        colors[22] = Color.yellow;
        colors[23] = Color.yellow;

        colors[24] = Color.gray;
        colors[25] = Color.gray;
        colors[26] = Color.gray;
        colors[27] = Color.gray;

        colors[28] = Color.magenta;
        colors[29] = Color.magenta;
        colors[30] = Color.magenta;
        colors[31] = Color.magenta;

        colors[32] = Color.black;
        colors[33] = Color.black;
        colors[34] = Color.black;
        colors[35] = Color.black;

        colors[36] = Color.white;
        colors[37] = Color.white;
        colors[38] = Color.white;
        colors[39] = Color.white;

        colors[40] = Color.clear;
        colors[41] = Color.clear;
        colors[42] = Color.clear;
        colors[43] = Color.clear;
        //Get components
        rtransform = GetComponent<RectTransform>();
        imageRenderer = GetComponent<Image>();
        cam = GameObject.Find("Main Camera").GetComponent<CameraController>();
        comboScript = GameObject.Find("Combo Counter").GetComponent<ComboController>();
        //Randomize length, speed, and color
        offset = Random.Range(-640f, 640f);
        length = Random.Range(400f, 600f);
        speed = Random.Range(5f, 15f);
        color = Random.Range(0, 4);
        color += Mathf.Min((comboScript.combo / 10) * 4, 40);
        col = colors[color];
        //Set color
        imageRenderer.color = col;
        //Set y and x position
        transform.localPosition = new Vector3(offset, 700, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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

    void Update()
    {
        if ((comboScript.reset == true) || (comboScript.smallReset == true)) resetting = true;
        if ((comboScript.tens == true) && (comboScript.combo <= 100)) increasing = true;

        if (increasing == true)
        {
            increment += 0.01f;
            col = Color.Lerp(colors[color + colorMod], colors[color + colorMod + 4], increment);
            imageRenderer.color = col;
            if (increment >= 1f)
            {
                colorMod += 4;
                col = colors[color + colorMod];
                imageRenderer.color = col;
                increment = 0f;
                increasing = false;
            }
        }

        if (resetting == true)
        {
            increment += 0.01f;
            col = Color.Lerp(colors[color + colorMod], colors[color], increment);
            imageRenderer.color = col;
            if (increment >= 1f)
            {
                colorMod = 0;
                col = colors[color];
                imageRenderer.color = col;
                increment = 0f;
                increasing = false;
            }
        }
    }
}
