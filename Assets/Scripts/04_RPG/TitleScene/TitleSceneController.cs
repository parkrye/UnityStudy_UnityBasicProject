using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneController : MonoBehaviour
{
    public void OnStartButton()
    {
        SceneManager.LoadScene(5);
    }

    public void OnQuitButton()
    {
        SceneManager.LoadScene(0);
    }
}
