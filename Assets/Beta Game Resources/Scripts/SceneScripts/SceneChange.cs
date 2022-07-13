using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    // a public string value type variable
    public string _scene;

    // a Method with a string value type argument to be used with UI
    public void SceneChanger(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    // a Method to be used in GameObjects
    public void SceneChanger()
    {
        SceneManager.LoadScene(_scene, LoadSceneMode.Single);
    }

    // A simple method which quits the application/game
    public void QuitGame()
    {
        Application.Quit();
    }

}
