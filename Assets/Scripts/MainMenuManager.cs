using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneRef.GameScene);
    }
    public void ExitGame()
    {
        print("APPLICATION QUIT");
        Application.Quit();
    }
}
