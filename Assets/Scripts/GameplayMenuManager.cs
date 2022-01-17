using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayMenuManager : MenuManager_Parent
{
    public static GameplayMenuManager _instance;

    [SerializeField] CanvasGroup speechLayer;


    [SerializeField] CanvasGroup gameplayLayer;
    [SerializeField] CanvasGroup endGameLayer;

    [SerializeField] GameObject win, lose;

    [SerializeField] GameObject controlls;
    bool isOptionsOpen = false;
    [SerializeField] float duration = 1f;

    bool goOn = false;

    void Awake()
    {
        _instance = this;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Init()
    {
        gameplayLayer.CanvasGroupFade(1);
        gameplayLayer.CanvasGroupInteractable(true);

        endGameLayer.CanvasGroupFade(0);
        endGameLayer.CanvasGroupInteractable(false);

        if (currentCoroutine == null)
            currentCoroutine = StartCoroutine(Initialize());
    }

    public void GoOn()
    {
        goOn = true;
    }

    protected override IEnumerator Initialize()
    {
        GameState._instance.ChangeState(Game_State.Message);
        MoneyManager._instance.Init();
        yield return StartCoroutine(TransitionManager._instance.FadeOutEffect());
        yield return new WaitForSeconds(0.5f);

        //Show Message
        yield return new WaitUntil(() => goOn);

        speechLayer.CanvasGroupFade(0);
        speechLayer.CanvasGroupInteractable(false);

        GameState._instance.ChangeState(Game_State.Gameplay);

        currentCoroutine = null;
    }

    #region EndScreen
    public void LoadEndScreen(bool _)
    {
        if (currentCoroutine == null)
            currentCoroutine = StartCoroutine(Load_EndScreen(_));
    }

    IEnumerator Load_EndScreen(bool _)
    {
        GameState._instance.ChangeState(Game_State.End);

        if (_)
            lose.SetActive(false);
        else
            win.SetActive(false);

        for (float i = duration; i > 0; i -= Time.deltaTime)
        {
            gameplayLayer.CanvasGroupFade(i);
            endGameLayer.CanvasGroupFade(duration - i);
            yield return null;
        }

        gameplayLayer.CanvasGroupInteractable(false);
        endGameLayer.CanvasGroupInteractable(true);
        controlls.SetActive(false);

        currentCoroutine = null;
    }

    #endregion
}
