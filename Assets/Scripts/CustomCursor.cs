using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomCursor : MonoBehaviour
{


    void Start()
    {

        Cursor.visible = false;

#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
        this.gameObject.SetActive(true);
#elif UNITY_IOS || UNITY_ANDROID
        this.gameObject.SetActive(false);
#endif

    }

    void Update()
    {

        Vector2 cursorPos = Input.mousePosition;
        transform.position = cursorPos;

    }
}
