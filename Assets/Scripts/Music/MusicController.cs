using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

    //Initialize variables
    AudioSource source;
    AudioClip[] music = new AudioClip[7];
    AudioClip crash;
    AudioClip crashShort;
    ComboController cc;
    int currentSong = 1;

	// Use this for initialization
	void Start () {
        //Get components and resources
        source = GetComponent<AudioSource>();
        cc = GameObject.Find("Combo Counter").GetComponent<ComboController>();
        crash = (AudioClip)Resources.Load("Music/crash_ext");
        crashShort = (AudioClip)Resources.Load("Music/crash_single");

        //Run loop to get riffs
        for (int i = 1; i <= 6; i++)
        {
            music[i] = (AudioClip)Resources.Load("Music/riff_" + i);
        }

        //Start playing first riff
        source.loop = true;
        source.clip = music[1];
        source.Play();
        source.volume = Settings.settings[0];
	}
	
	// Update is called once per frame
	void Update () {
        //Check where combo is and assign riff
        switch (cc.combo)
        {
            case 0:
                source.loop = false;
                currentSong = 1;
                break;

            case 5:
                source.loop = false;
                currentSong = 2;
                break;

            case 10:
                source.loop = false;
                currentSong = 3;
                break;

            case 20:
                source.loop = false;
                currentSong = 4;
                break;

            case 30:
                source.loop = false;
                currentSong = 5;
                break;

            case 50:
                source.loop = false;
                currentSong = 6;
                break;
        }

        //Change track at end of loop
        if (source.loop == false)
        {
            if (source.isPlaying == false)
            {
                source.clip = music[currentSong];
                source.loop = true;
                source.Play();
            }
        }

        //Reset music if small combo is lost
        if (cc.smallReset == true)
        {
            source.Stop();
            source.clip = crashShort;
            source.Play();
            cc.reset = false;
            cc.smallReset = false;
        }

        //Reset music if big combo is lost
        if (cc.reset == true)
        {
            source.Stop();
            source.clip = crash;
            source.Play();
            cc.reset = false;
            cc.smallReset = false;
        }

        //Stop music if player dead
        if (!RuntimeDictionary.RuntimeObjects.ContainsKey("Player") && source.clip != crash && source.clip != crashShort) source.Stop();
	}
}
