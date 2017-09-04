using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CameraController : MonoBehaviour
{

    //Initialize variables
    public Vector3 target;
    GameObject player;
    GameObject cursor;
    protected Camera cam;
    protected Vector3 currentVelocity;
    public float intensity = 0;
    float multiplier;
    public Rect limits;
    public float maxRadius = 1f;

    // Use this for initialization
    void Start()
    {
        //Set velocity
        currentVelocity = Vector3.zero;

        //Set shake multiplier
        multiplier = Settings.settings[2] * 2f;

        //Get camera
        cam = GetComponent<Camera>();
        if (!cam.orthographic)
        {
            Debug.LogError("this script requires an orthographic camera!");
            Destroy(this);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (RuntimeDictionary.RuntimeObjects.ContainsKey("Player"))
        {
            //Assign tethers
            if (player == null) player = GameObject.Find("Player(Clone)");
            if (cursor == null) cursor = GameObject.Find("Crosshair(Clone)");

            //Find new camera position
            Vector3 current = transform.position;
            Vector3 midway = (player.transform.position + cursor.transform.position) * 0.5f;
            float length = Vector2.Distance(player.transform.position, cursor.transform.position) * 0.5f;
#if UNITY_STANDALONE
            if (length <= maxRadius)
            {
                target = midway;
            }
            else
            {
                Vector3 centToMid = midway - player.transform.position;
                centToMid *= maxRadius / length;
                target = player.transform.position + centToMid;
            }
#elif UNITY_ANDROID
            target.x = player.transform.position.x;
            target.y = player.transform.position.y;
#endif
            target.z = -10;

            //Smoothly transition to new position within boundaries
            target.x = Mathf.Clamp(target.x, limits.min.x, limits.max.x);
            target.y = Mathf.Clamp(target.y, limits.min.y, limits.max.y);
            transform.position = Vector3.SmoothDamp(current, target, ref currentVelocity, 0.1f);


            //Add shake
            Vector3 shake = Random.insideUnitSphere * intensity * multiplier;
            cam.transform.localPosition += shake;
            intensity *= 0.85f;
            if (intensity < 0.001f) intensity = 0;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(CameraController))]
public class CameraEditor : Editor
{
    public void OnSceneGUI()
    {
        CameraController cam = target as CameraController;

        Color transp = new Color(0, 0, 0, 0);
        Handles.DrawSolidRectangleWithOutline(cam.limits, transp, Color.red);
        Handles.DrawWireArc(cam.transform.position, cam.transform.forward, -cam.transform.right, 360, cam.maxRadius);
    }
}
#endif