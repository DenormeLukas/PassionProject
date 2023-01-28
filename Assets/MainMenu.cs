using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MainMenu : MonoBehaviour
{

    public string scene;

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(scene);
    }

}
