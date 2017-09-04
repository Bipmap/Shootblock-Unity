using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SaveBack : MonoBehaviour {

#if UNITY_STANDALONE
    //Initialize vairables
    Slider musicSlider;
    Slider sensSlider;
    Slider cameraSlider;
    byte[] setLevel = new byte[3];

	// Use this for initialization
	void Start () {
        //Read levels if they exist
        if (File.Exists("Settings"))
        {
            FileStream filestream = new FileStream("Settings", FileMode.Open, FileAccess.Read, FileShare.None);
            NetReader reader = new NetReader(filestream);
            reader.readByteArray(ref setLevel, 0, setLevel.Length);
            filestream.Close();
        }
        else
        {
            //Load up array
            setLevel[0] = 100;
            setLevel[1] = 30;
            setLevel[2] = 50;
            //Create file if nonexistent
            FileStream filestream = new FileStream("Settings", FileMode.Create, FileAccess.ReadWrite, FileShare.None);
            NetWriter writer = new NetWriter(filestream);
            writer.writeByteArray(ref setLevel, 0, setLevel.Length);
            filestream.Close();
            //Set default slider values
            Settings.settings[0] = 1f;
            Settings.settings[1] = 0.3f;
            Settings.settings[2] = 0.5f;
        }

        //Get components
        musicSlider = GameObject.Find("Music Slider").GetComponent<Slider>();
        sensSlider = GameObject.Find("Sensitivity Slider").GetComponent<Slider>();
        cameraSlider = GameObject.Find("Camera Shake Slider").GetComponent<Slider>();

        //Set levels
        musicSlider.value = setLevel[0] / 100f;
        sensSlider.value = setLevel[1] / 100f;
        cameraSlider.value = setLevel[2] / 100f;
    }
	
	public void OptionsExit()
    {
        //Get levels
        setLevel[0] = (byte)(musicSlider.value * 100);
        setLevel[1] = (byte)(sensSlider.value * 100);
        setLevel[2] = (byte)(cameraSlider.value * 100);

        //Save
        FileStream filestream = new FileStream("Settings", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
        NetWriter writer = new NetWriter(filestream);
        writer.writeByteArray(ref setLevel, 0, setLevel.Length);
        filestream.Close();

        //Apply to array
        Settings.settings[0] = musicSlider.value;
        Settings.settings[1] = sensSlider.value;
        Settings.settings[2] = cameraSlider.value;

        //Move
        MenuNavigator move = GameObject.Find("LevelManager").GetComponent<MenuNavigator>();
        move.MoveD();
    }

#elif UNITY_ANDROID
    //Initialize vairables
    Slider musicSlider;
    OptionsPlatform options;
    Slider cameraSlider;
    byte[] setLevel = new byte[4];

    // Use this for initialization
    void Start()
    {
        //Read levels if they exist
        if (File.Exists(Application.persistentDataPath + "Settings"))
        {
            FileStream filestream = new FileStream(Application.persistentDataPath + "Settings", FileMode.Open, FileAccess.Read, FileShare.None);
            NetReader reader = new NetReader(filestream);
            reader.readByteArray(ref setLevel, 0, setLevel.Length);
            filestream.Close();
        }
        else
        {
            //Load up array
            setLevel[0] = 100;
            setLevel[1] = 30;
            setLevel[2] = 50;
            setLevel[3] = 6;
            //Create file if nonexistent
            FileStream filestream = new FileStream(Application.persistentDataPath + "Settings", FileMode.Create, FileAccess.ReadWrite, FileShare.None);
            NetWriter writer = new NetWriter(filestream);
            writer.writeByteArray(ref setLevel, 0, setLevel.Length);
            filestream.Close();
            //Set default slider values
            Settings.settings[0] = 1f;
            Settings.settings[1] = 0.3f;
            Settings.settings[2] = 0.5f;
            Settings.settings[3] = 0.06f;
        }

        //Get components
        musicSlider = GameObject.Find("Music Slider").GetComponent<Slider>();
        cameraSlider = GameObject.Find("Camera Shake Slider").GetComponent<Slider>();
        options = GameObject.Find("Options").GetComponent<OptionsPlatform>();

        //Set levels
        musicSlider.value = setLevel[0] / 100f;
        cameraSlider.value = setLevel[2] / 100f;
    }

    public void OptionsExit()
    {
        //Get levels
        setLevel[0] = (byte)(musicSlider.value * 100);
        setLevel[1] = (byte)(options.maxTilt * 100);
        setLevel[2] = (byte)(cameraSlider.value * 100);
        setLevel[3] = (byte)(options.deadzoneTilt * 100);

        //Save
        FileStream filestream = new FileStream(Application.persistentDataPath + "Settings", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
        NetWriter writer = new NetWriter(filestream);
        writer.writeByteArray(ref setLevel, 0, setLevel.Length);
        filestream.Close();

        //Apply to array
        Settings.settings[0] = musicSlider.value;
        Settings.settings[1] = options.maxTilt;
        Settings.settings[2] = cameraSlider.value;
        Settings.settings[3] = options.deadzoneTilt;

        //Move
        MenuNavigator move = GameObject.Find("LevelManager").GetComponent<MenuNavigator>();
        move.MoveD();
    }
#endif
}
