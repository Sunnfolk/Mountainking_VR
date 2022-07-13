using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName ="UI voids",menuName ="Create UI voids")]
public class UIVariables : ScriptableObject
{
    public void OpenMenu(GameObject openMenu)
    {
        openMenu.SetActive(true);
    }

    public void CloseMenu(GameObject closeMenu)
    {
        closeMenu.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void English()
    {
        PlayerBehaviour._instance.GetComponentInChildren<Narrator>().SetLanguage(Language.ENGLISH);
    }
    public void Norsk()
    {
        PlayerBehaviour._instance.GetComponentInChildren<Narrator>().SetLanguage(Language.NORWEGIAN);
    }

    public void SetMasterVolume(int volume)
    {

    }

    public void SetMusicVolume(int volume)
    {

    }

    public void SetVoiceVolume(int volume)
    {

    }

    public void SetSFXVolume(int volume)
    {

    }

}
