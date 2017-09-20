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

        colors[4] = new Color(0.514f, 0.749f, 0.396f, alpha);
        colors[5] = new Color(0.616f, 0.82f, 0.514f, alpha);
        colors[6] = new Color(0.682f, 0.886f, 0.631f, alpha);
        colors[7] = new Color(0.498f, 0.804f, 0.608f, alpha);

        colors[8] = new Color(0.31f, 0.486f, 0.675f, alpha);
        colors[9] = new Color(0.62f, 0.937f, 0.898f, alpha);
        colors[10] = new Color(0.753f, 0.878f, 0.871f, alpha);
        colors[11] = new Color(0.302f, 0.957f, 0.875f, alpha);

        colors[12] = new Color(0.439f, 0.216f, 0.804f, alpha);
        colors[13] = new Color(0.314f, 0.431f, 0.898f, alpha);
        colors[14] = new Color(0.408f, 0.698f, 0.973f, alpha);
        colors[15] = new Color(0.255f, 0.58f, 0.886f, alpha);

        colors[16] = new Color(0.898f, 0.42f, 0.439f, alpha);
        colors[17] = new Color(0.953f, 0.569f, 0.627f, alpha);
        colors[18] = new Color(0.961f, 0.89f, 0.878f, alpha);
        colors[19] = new Color(0.898f, 0.125f, 0.243f, alpha);

        colors[20] = new Color(0.424f, 0.831f, 1, alpha);
        colors[21] = new Color(0.933f, 0.898f, 0.914f, alpha);
        colors[22] = new Color(0.718f, 0.808f, 0.808f, alpha);
        colors[23] = new Color(0.659f, 0.69f, 0.698f, alpha);

        colors[24] = new Color(0.667f, 0.325f, 0.953f, alpha);
        colors[25] = new Color(0.718f, 0.267f, 0.722f, alpha);
        colors[26] = new Color(0.639f, 0.475f, 0.788f, alpha);
        colors[27] = new Color(0.678f, 0.725f, 0.89f, alpha);

        colors[28] = new Color(0.165f, 0.118f, 0.361f, alpha);
        colors[29] = new Color(0.855f, 0.769f, 0.969f, alpha);
        colors[30] = new Color(0.09f, 0.722f, 0.565f, alpha);
        colors[31] = new Color(0.933f, 0.259f, 0.4f, alpha);

        colors[32] = new Color(1, 0, 0.435f, alpha);
        colors[33] = new Color(1, 0.702f, 0, alpha);
        colors[34] = new Color(1, 0.961f, 0.22f, alpha);
        colors[35] = new Color(0.525f, 0.086f, 0.341f, alpha);

        colors[36] = new Color(0.776f, 0.78f, 0.769f, alpha);
        colors[37] = new Color(0.855f, 0.855f, 0.855f, alpha);
        colors[38] = new Color(0.518f, 0.416f, 0.416f, alpha);
        colors[39] = new Color(0.973f, 0.957f, 0.89f, alpha);

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
