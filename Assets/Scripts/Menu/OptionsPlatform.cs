using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPlatform : MonoBehaviour {

    //Initialize variables
    GameObject sensSlider;
    GameObject recalibrate;
    Slider music;
    Slider cameraShake;
    Image fade;
    Button recalibrateButton;
    Button back;
    GameObject maxInstructions;
    GameObject maxFeedback;
    GameObject maxConfirm;
    Slider maxFeedbackSlider;
    GameObject deadzoneInstructions;
    GameObject deadzoneFeedback;
    GameObject deadzoneConfirm;
    Slider deadzoneFeedbackSlider;
    public float maxTilt;
    public float deadzoneTilt;

	// Use this for initialization
	void Start () {
        //Find components... all of them
        sensSlider = transform.Find("Sensitivity Slider").gameObject;
        recalibrate = transform.Find("Recalibrate Button").gameObject;
        music = transform.Find("Music Slider").GetComponent<Slider>();
        cameraShake = transform.Find("Camera Shake Slider").GetComponent<Slider>();
        fade = transform.Find("Fade").GetComponent<Image>();
        recalibrateButton = recalibrate.GetComponent<Button>();
        back = transform.Find("Back").GetComponent<Button>();
        maxInstructions = transform.Find("Max Instructions").gameObject;
        maxFeedback = transform.Find("Max Feedback").gameObject;
        maxFeedbackSlider = maxFeedback.GetComponent<Slider>();
        maxConfirm = transform.Find("Max Confirm").gameObject;
        deadzoneInstructions = transform.Find("Deadzone Instructions").gameObject;
        deadzoneFeedback = transform.Find("Deadzone Feedback").gameObject;
        deadzoneFeedbackSlider = deadzoneFeedback.GetComponent<Slider>();
        deadzoneConfirm = transform.Find("Deadzone Confirm").gameObject;

        //Disable components
        maxInstructions.SetActive(false);
        maxFeedback.SetActive(false);
        maxConfirm.SetActive(false);
        deadzoneInstructions.SetActive(false);
        deadzoneFeedback.SetActive(false);
        deadzoneConfirm.SetActive(false);

        //Platform specific options
#if UNITY_STANDALONE
        sensSlider.SetActive(true);
        recalibrate.SetActive(false);
#elif UNITY_ANDROID
        sensSlider.SetActive(false);
        recalibrate.SetActive(true);
#endif
    }

    public void OnRecalibratePress()
    {
        music.interactable = false;
        cameraShake.interactable = false;
        recalibrateButton.interactable = false;
        back.interactable = false;
        fade.enabled = true;
        maxInstructions.SetActive(true);
        maxFeedback.SetActive(true);
        maxConfirm.SetActive(true);
    }

    public void OnMaximumPress()
    {
        maxTilt = Mathf.Abs(Input.acceleration.x);
        maxInstructions.SetActive(false);
        maxFeedback.SetActive(false);
        maxConfirm.SetActive(false);

        deadzoneFeedbackSlider.maxValue = maxTilt;
        deadzoneInstructions.SetActive(true);
        deadzoneFeedback.SetActive(true);
        deadzoneConfirm.SetActive(true);
    }

    public void OnDeadzonePress()
    {
        deadzoneTilt = Mathf.Abs(Input.acceleration.x);
        deadzoneInstructions.SetActive(false);
        deadzoneFeedback.SetActive(false);
        deadzoneConfirm.SetActive(false);

        music.interactable = true;
        cameraShake.interactable = true;
        recalibrateButton.interactable = true;
        back.interactable = true;
        fade.enabled = false;
    }

    void Update()
    {
        if (maxFeedback.activeSelf == true)
        {
            maxFeedbackSlider.value = Mathf.Abs(Input.acceleration.x);
        }

        if (deadzoneFeedback.activeSelf == true)
        {
            deadzoneFeedbackSlider.value = Mathf.Abs(Input.acceleration.x);
        }
    }

}
