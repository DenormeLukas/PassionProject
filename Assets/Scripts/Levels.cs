using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Levels : MonoBehaviour
{

    public Button button;
    public RawImage rawImage;

    public Button button2;
    public RawImage rawImage2;

    public Button button3;
    public RawImage rawImage3;


    void Start()
    {

    }

    void Update()
    {

        if (Movement.l2)
        {
            rawImage2.color = Color.black;
            button2.interactable = true;
        }
        else
        {
            rawImage2.color = Color.grey;
        }


        if (Movement.l3)
        {
            rawImage3.color = Color.black;
            button3.interactable = true;
        }
        else
        {
            rawImage3.color = Color.grey;
        }

    }
}
