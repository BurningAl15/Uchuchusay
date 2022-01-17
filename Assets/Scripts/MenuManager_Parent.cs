using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager_Parent : MonoBehaviour
{
    protected Coroutine currentCoroutine = null;

    protected virtual void Start()
    {
        Init();
    }

    protected virtual void Init() { }

    protected virtual IEnumerator Initialize()
    {
        MoneyManager._instance.Init();

        yield return StartCoroutine(TransitionManager._instance.FadeOutEffect());
        yield return new WaitForSeconds(0.5f);

        currentCoroutine = null;
    }

    /*
    MainMenu
    LevelSelector
    CharacterSelector
    Level
*/
    #region Scenes
    public void LoadOption(int sceneType)
    {
        if (currentCoroutine == null)
            currentCoroutine = StartCoroutine(Load_Option(sceneType));
    }

    IEnumerator Load_Option(int sceneType)
    {
        yield return StartCoroutine(TransitionManager._instance.FadeInEffect());
        yield return new WaitForSeconds(0.5f);

        switch (sceneType)
        {
            case 0:
                SceneUtils.LoadMainScene();
                break;
            case 1:
                SceneUtils.LoadLevelSelector();
                break;
            case 2:
                SceneUtils.LoadLevel(1);
                break;
            case 3:
                SceneUtils.LoadCharacterSelector();
                break;
            default:
                SceneUtils.QuitGame();
                break;
        }
        currentCoroutine = null;
    }
    #endregion

}
