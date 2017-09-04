using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw
{
    public static void DrawEllipse(float xradius, float yradius, int segments, float thickness, LineRenderer line)
    {
        line.positionCount = segments + 1;
        line.useWorldSpace = false;
        line.startWidth = thickness;
        line.endWidth = thickness;
        float x;
        float y;
        float z = 0f;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

            line.SetPosition(i, new Vector3(x, y, z));

            angle += (360f / segments);
        }
    }

    public static void DrawRectangle(float width, float height, float thickness, LineRenderer line)
    {
        line.positionCount = 4;
        line.useWorldSpace = false;
        line.startWidth = thickness;
        line.endWidth = thickness;
        line.loop = true;

        line.SetPosition(0, new Vector3(0 - width/2, 0 - height/2, 0));
        line.SetPosition(1, new Vector3(width/2, 0 - height/2, 0));
        line.SetPosition(2, new Vector3(width/2, height/2, 0));
        line.SetPosition(3, new Vector3(0 - width/2, height/2, 0));
    }
}
