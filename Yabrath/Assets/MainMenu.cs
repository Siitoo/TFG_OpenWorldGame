using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true;
    }


    public void OnPlay()
    {
        Cursor.visible = false;
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
