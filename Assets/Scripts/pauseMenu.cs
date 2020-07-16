using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenu : MonoBehaviour
{
    SceneMan man;
    private void Awake()
    {
        man = FindObjectOfType<SceneMan>();
    }
    private void OnEnable()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Resume()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameObject.SetActive(false);
    }
    public void Restart()
    {
        Time.timeScale = 1;
        man.LoadLevel(1);
    }
    public void menu()
    {
        Time.timeScale = 1;
        man.LoadLevel(0);
    }
}
