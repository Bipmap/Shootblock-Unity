using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Settings : MonoBehaviour {
#if UNITY_STANDALONE
    public static float[] settings = new float[3];
    byte[] setLevel = new byte[3];

    void Start()
    {
        FileStream filestream = new FileStream("Settings", FileMode.Open, FileAccess.Read, FileShare.None);
        NetReader reader = new NetReader(filestream);
        reader.readByteArray(ref setLevel, 0, setLevel.Length);
        filestream.Close();

        settings[0] = setLevel[0] / 100f;
        settings[1] = setLevel[1] / 100f;
        settings[2] = setLevel[2] / 100f;
    }
#elif UNITY_ANDROID
    public static float[] settings = new float[4];
    byte[] setLevel = new byte[4];

    void Start()
    {
        FileStream filestream = new FileStream(Application.persistentDataPath + "Settings", FileMode.Open, FileAccess.Read, FileShare.None);
        NetReader reader = new NetReader(filestream);
        reader.readByteArray(ref setLevel, 0, setLevel.Length);
        filestream.Close();

        settings[0] = setLevel[0] / 100f;
        settings[1] = setLevel[1] / 100f;
        settings[2] = setLevel[2] / 100f;
        settings[3] = setLevel[3] / 100f;
    }
#endif
}
