using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;
public class SceneMan : MonoBehaviour
{
    public int graphicsIndex = 5;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        SetQuality(graphicsIndex);
    }
    public void SetQuality(int index)
    {
        graphicsIndex = index;
        QualitySettings.SetQualityLevel(graphicsIndex);
        Camera.main.GetComponent<PostProcessVolume>().enabled = (!(graphicsIndex <= 3));
    }
    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
}
