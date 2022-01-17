using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneUtils
{
    public static void LoadMainScene()
    {
        SceneManager.LoadScene(constants.mainScene);
    }

    public static void LoadLevelSelector()
    {
        SceneManager.LoadScene(constants.levelSelector);
    }

    public static void LoadLevel(int _index)
    {
        SceneManager.LoadScene(constants.level + _index);
    }

    public static void LoadCharacterSelector()
    {
        SceneManager.LoadScene(constants.characterSelector);
    }

    public static void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void QuitGame()
    {
        Application.Quit();
    }
}
