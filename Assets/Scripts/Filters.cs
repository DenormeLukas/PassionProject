using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Filters : MonoBehaviour
{
    public KeyCode toggleKey = KeyCode.E;
    public float transitionDuration = 1.0f;
    public float targetExposure = -10f;

    public KeyCode toggleKey2 = KeyCode.R;
    private bool depthOfFieldEnabled = false;
    private DepthOfField depthOfField;

    public KeyCode timeKey = KeyCode.T;
    public float slowMotionTime = 2.0f;
    private bool coolDown = false;

    private VolumeProfile volumeProfile;
    private bool increasing = false;
    private float currentExposure;
    private float targetTime;

    static public bool isDark;
    static public bool isBlurred;

    public Slider coolDownSlider;
    private float targetProgress = 0;

    void Start()
    {
        volumeProfile = GetComponent<Volume>().profile;
        volumeProfile.TryGet(out ColorAdjustments colorAdjustments);
        volumeProfile.TryGet(out depthOfField);

        currentExposure = colorAdjustments.postExposure.value;

        isDark = false;
        isBlurred = false;

        coolDownSlider.value = 1.0f;

      //  coolDownSlider = gameObject.GetComponent<Slider>();
    }

    void Update()
    {

        if (Input.GetKeyDown(toggleKey))
        {
            increasing = !increasing;
            targetTime = Time.time + transitionDuration;
        }

        if (Input.GetKeyDown(toggleKey2))
        {
            if(isBlurred)
            {
                isBlurred = false;
            } else
            {
                isBlurred = true;
            }

            depthOfFieldEnabled = !depthOfFieldEnabled;
            depthOfField.active = depthOfFieldEnabled;
            
        }

        if (Input.GetKeyDown(timeKey))
        {
            if(!coolDown)
            {
                Time.timeScale = 0.5f;
                Invoke("NormalSpeed", slowMotionTime);
                coolDownSlider.value = 0.01f;
                coolDown = true;
            }       

        }

        if (increasing)
        {
            currentExposure = Mathf.Lerp(currentExposure, targetExposure, (targetTime - Time.time) / transitionDuration);
            isDark = true;
        }
        else
        {
            currentExposure = Mathf.Lerp(currentExposure, 0, (targetTime - Time.time) / transitionDuration);
            isDark = false;
        }

        volumeProfile.TryGet(out ColorAdjustments colorAdjustments);
        colorAdjustments.postExposure.value = currentExposure;

        //Smooth animation to fill up cooldown slider
        if(coolDownSlider.value < targetProgress)
        {
            coolDownSlider.value += 0.075f * Time.deltaTime;
        }
    }

    private void NormalSpeed()
    {
        Time.timeScale = 1.0f;


         CoolDownSlider(1.0f);

         if (coolDownSlider.value == 1.0f)
         {
                coolDown = false;
         }
        
    }

    private void CoolDownSlider(float newProgress)
    {
        targetProgress = coolDownSlider.value + newProgress;
    }
}
