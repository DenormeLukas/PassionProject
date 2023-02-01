using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MainMenu : MonoBehaviour
{

    public string tutorial;
    public string tutorialMobile;
    public string levels;
    public string credits;
    public string main;
    public string l1;
    public string l2;
    public string l3;


    public void LoadTutorial()
    {
#if !UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_WEBGL
        SceneManager.LoadScene(tutorial);
        #elif UNITY_IOS || UNITY_ANDROID
        SceneManager.LoadScene(tutorialMobile);
        #endif
    }

    public void LoadLevels()
    {
        SceneManager.LoadScene(levels);
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene(credits);
    }

    public void LoadMain()
    {
        SceneManager.LoadScene(main);
    }

    public void Load1()
    {
        SceneManager.LoadScene(l1);
    }

    public void Load2()
    {
        SceneManager.LoadScene(l2);
    }

    public void Load3()
    {
        SceneManager.LoadScene(l3);
    }

}
