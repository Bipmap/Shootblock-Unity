using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrosshairController : MonoBehaviour
{
    //Initialize variables
    Camera cam;
    Renderer rend;
    Vector3 touchPos;
    Vector3 mousePos;
    Transform player;
    SpriteRenderer crosshair;
    float sens;
    Vector3 touchLoc;
    GameObject dot;
    PlayerGunController gunScript;
    LineRenderer line;
    PlatformInput touch;

    //Assign variables
    void Start()
    {
        //Assign sensitivity
        sens = Settings.settings[1];
        //Get components and gameobjects
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        player = transform.parent.Find("Player(Clone)");
        gunScript = transform.parent.Find("Gun(Clone)").GetComponent<PlayerGunController>();
        rend = GetComponent<Renderer>();
        touchPos.z = 0;
        crosshair = GetComponent<SpriteRenderer>();
        dot = Resources.Load<GameObject>("Dot");
        line = GetComponent<LineRenderer>();
        touch = GameObject.Find("Input Handler").GetComponent<PlatformInput>();

#if UNITY_STANDALONE
        Cursor.lockState = CursorLockMode.Locked;
#endif
    }

    //Render platform-dependent crosshair
    void Update()
    {
#if UNITY_STANDALONE
        //Get mouse input
        mousePos.x = Input.GetAxis("Mouse X") * sens;
        mousePos.y = Input.GetAxis("Mouse Y") * sens;
        //Compensate for camera movement
        rend.transform.position += mousePos + (Time.deltaTime * cam.velocity);
        //Clamp to screen
        Vector2 clamper = new Vector2(Mathf.Clamp(rend.transform.position.x,
            cam.transform.position.x - cam.orthographicSize * cam.aspect, cam.transform.position.x + cam.orthographicSize * cam.aspect),
            Mathf.Clamp(rend.transform.position.y, cam.transform.position.y - cam.orthographicSize, cam.transform.position.y + cam.orthographicSize));
        rend.transform.position = clamper;

        //Render crosshair based on gun type
        switch (gunScript.currentGun)
        {
            case PlayerGunController.Gun.pistol:
            {
                //Render crosshair
                int segments = 56;
                float radius = 0.1f;
                float thickness = 0.03f;
                line.enabled = true;
                Draw.DrawEllipse(radius, radius, segments, thickness, line);
                //Unrender dot
                crosshair.enabled = false;
            }
            break;
            
            case PlayerGunController.Gun.shotgun:
            {
                //Render crosshair
                int segments = 56;
                float radius = Vector2.Distance(transform.position, player.transform.position) / 3;
                float thickness = 0.03f;
                line.enabled = true;
                Draw.DrawEllipse(radius, radius, segments, thickness, line);
                //Render dot
                crosshair.enabled = true;
            }
            break;

            case PlayerGunController.Gun.smg:
            {
                //Render crosshair
                float radius = Vector2.Distance(transform.position, player.transform.position) / 2.5f;
                float thickness = 0.03f;
                line.enabled = true;
                Draw.DrawRectangle(radius, radius, thickness, line);
                //Render dot
                crosshair.enabled = true;
            }
            break;

            case PlayerGunController.Gun.zooka:
            {
                //Render crosshair
                float radius = 0.2f;
                float thickness = 0.03f;
                line.enabled = true;
                Draw.DrawRectangle(radius, radius, thickness, line);
                //Render dot
                crosshair.enabled = false;
            }
            break;

            case PlayerGunController.Gun.revolver:
            {
                //Render crosshair
                float radius = 0.16f;
                float thickness = 0.03f;
                line.enabled = true;
                Draw.DrawEllipse(radius, radius, 4, thickness, line);
                //Render dot
                crosshair.enabled = false;
            }
            break;

            case PlayerGunController.Gun.spingun:
            {
                //Render crosshair
                float radius = Vector2.Distance(transform.position, player.transform.position) / gunScript.spinDelay;
                float thickness = 0.03f;
                line.enabled = true;
                Draw.DrawEllipse(radius, radius, 8, thickness, line);
                //Render dot
                crosshair.enabled = true;
            }
            break;

            default:
            {
                crosshair.enabled = true;
                line.enabled = false;
            }
            break;
        }
        

#elif UNITY_ANDROID
        if ((touch.input == PlatformInput.InputState.beginShoot) || (touch.input == PlatformInput.InputState.shooting))
        {
            touchLoc = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 1);
            GameObject location = Instantiate(dot);
            location.transform.position = cam.ScreenToWorldPoint(touchLoc);
            crosshair.transform.position = location.transform.position;
        }
#endif
    }
}