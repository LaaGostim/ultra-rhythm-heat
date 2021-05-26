using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScriptHandler : MonoBehaviour
{
    public Animator transiction;

    public void PlayGame()
    {
        Debug.Log("PLAY GAME");
        StartCoroutine(LoadLevel(2));
    }

    public void SettingsMenu()
    {
        Debug.Log("SETTINGS");
        StartCoroutine(LoadLevel(1));
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void BackButton()
    {
        Debug.Log("CHANGED SCENE");
        StartCoroutine(LoadLevel(0));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transiction.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelIndex);

    }
}
