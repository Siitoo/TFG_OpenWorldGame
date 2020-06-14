using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public void OnPlay()
    {
        SceneManager.LoadScene(1); 
    }

    public void OnAbout()
    {

    }

    public void OnQuit()
    {
        Application.Quit();
    }

}
