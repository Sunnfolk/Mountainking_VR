using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class SceneVariables : ScriptableObject
{
    public void ChangeScenes(Scene scene)
    {
        SceneManager.LoadScene(scene.name);
    }

    public void ChangeScene(string value)
    {
        SceneManager.LoadScene(value);
    }
}
