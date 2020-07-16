using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    SceneMan man;
    private void Start()
    {
        man = FindObjectOfType<SceneMan>();
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void Play()
    {
        man.LoadLevel(1);
    }
}
