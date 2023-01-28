using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour
{
    static public float speed = 5.0f;
    public float increaseInterval = 10.0f;
    private float timeSinceIncrease;
    Scene currScene;

    void Start()
    {
        currScene = SceneManager.GetActiveScene();
        speed = 5.0f;
    }

    void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;

        if (currScene.name != "Tutorial")
        {
            timeSinceIncrease += Time.deltaTime;
            if (timeSinceIncrease >= increaseInterval)
            {
                speed += 0.5f;
                timeSinceIncrease = 0;
            }
        }

        if(Movement.isDeath)
        {
            speed = 0.0f;
        }

       
    }
}
