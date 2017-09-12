using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MenuNavigator : MonoBehaviour
{
    //Initialize variables
    Camera cam;
    Canvas canvas;
    RectTransform creditsBG;
    RectTransform optionsBG;
    Slider enemyAimSpeed;
    Slider enemyAimScalar;
    Slider comboTimerSpeed;
    ComboScalarToggle toggle;
    bool moveR = false;
    bool moveL = false;
    bool moveU = false;
    bool moveD = false;
    float xPos;
    float yPos;
    float dist = 0;

    void Start()
    {
        //Get components
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        creditsBG = GameObject.Find("Credits").transform.Find("Background").GetComponent<RectTransform>();
        optionsBG = GameObject.Find("Options").transform.Find("Background").GetComponent<RectTransform>();
        enemyAimSpeed = GameObject.Find("Enemy Aim Speed Slider").GetComponent<Slider>();
        enemyAimScalar = GameObject.Find("Enemy Aim Speed Scalar").GetComponent<Slider>();
        comboTimerSpeed = GameObject.Find("Combo Timer Speed").GetComponent<Slider>();
        toggle = GameObject.Find("Toggle").GetComponent<ComboScalarToggle>();
        //Set to world space and realign menus
        canvas.renderMode = RenderMode.WorldSpace;
        if (cam.aspect == 16f/10f)
        {
            creditsBG.offsetMin = new Vector2(0, -80);
            creditsBG.offsetMax = new Vector2(0, -80);
            optionsBG.offsetMin = new Vector2(0, 80);
            optionsBG.offsetMax = new Vector2(0, 80);
        }
        //Unlock mouse
#if UNITY_STANDALONE
        Cursor.lockState = CursorLockMode.None;
#endif
    }

    //Pick level 1
    public void LevelSelect1()
    {
        MoveR();
        StaticSettings.level = "Level 1";
    }

    public void LoadLevel()
    {
        StaticSettings.enemyAimSpeed = enemyAimSpeed.value;
        StaticSettings.enemyAimScalar = enemyAimScalar.value;
        StaticSettings.comboTimerSpeed = comboTimerSpeed.value;
        StaticSettings.comboTimerScalar = toggle.on;
        SceneManager.LoadScene(StaticSettings.level);
    }

    //Load the range
    public void LoadRange()
    {
        SceneManager.LoadScene("Range");
    }

    //Quit game
    public void Quit()
    {
        Application.Quit();
    }

    //Load webpages
    public void BipmapGames()
    {
        Application.OpenURL("https://github.com/Bipmap/");
    }

    public void RobinBlend()
    {
        Application.OpenURL("https://www.youtube.com/user/ThatOneKhajiit");
    }

    public void NetIO()
    {
        Application.OpenURL("http://github.com/stereorocker/netio");
    }

    //Move in a direction
    public void MoveR()
    {
        moveR = true;
        xPos = cam.transform.position.x;
    }

    public void MoveL()
    {
        moveL = true;
        xPos = cam.transform.position.x;
    }

    public void MoveU()
    {
        moveU = true;
        yPos = cam.transform.position.y;
    }

    public void MoveD()
    {
        moveD = true;
        yPos = cam.transform.position.y;
    }

    void Update()
    {
        //Execute movement
        if (moveR == true)
        {
            //Add lerp distance
            dist += 0.1f;
            //Move camera via lerp
            cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(xPos + (cam.orthographicSize * cam.aspect * 2),
                cam.transform.position.y, -10), dist);
            //If moved fully, movement = false, dist reset
            if (dist >= 1)
            {
                moveR = false;
                dist = 0;
            }
        }

        if (moveL == true)
        {
            dist += 0.1f;
            cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(xPos - (cam.orthographicSize * cam.aspect * 2),
                cam.transform.position.y, -10), dist);
            if (dist >= 1)
            {
                moveL = false;
                dist = 0;
            }
        }

        if (moveU == true)
        {
            dist += 0.1f;
            cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(cam.transform.position.x,
                yPos + (cam.orthographicSize * 2), -10), dist);
            if (dist >= 1)
            {
                moveU = false;
                dist = 0;
            }
        }

        if (moveD == true)
        {
            dist += 0.1f;
            cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(cam.transform.position.x,
                yPos - (cam.orthographicSize * 2), -10), dist);
            if (dist >= 1)
            {
                moveD = false;
                dist = 0;
            }
        }
    }
}
