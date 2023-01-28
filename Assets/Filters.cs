using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private VolumeProfile volumeProfile;
    private bool increasing = false;
    private float currentExposure;
    private float targetTime;

    static public bool isDark;
    static public bool isBlurred;

    void Start()
    {
        volumeProfile = GetComponent<Volume>().profile;
        volumeProfile.TryGet(out ColorAdjustments colorAdjustments);
        volumeProfile.TryGet(out depthOfField);
        currentExposure = colorAdjustments.postExposure.value;

        isDark = false;
        isBlurred = false;
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
    }
}
