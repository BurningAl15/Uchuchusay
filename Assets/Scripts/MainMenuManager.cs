using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MenuManager_Parent
{
    [SerializeField] CanvasGroup optionsLayer;
    [SerializeField] CanvasGroup storeLayer;
    bool isOptionsOpen = false;
    [SerializeField] float duration = 1f;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Init()
    {
        optionsLayer.CanvasGroupFade(1);
        optionsLayer.CanvasGroupInteractable(true);

        storeLayer.CanvasGroupFade(0);
        storeLayer.CanvasGroupInteractable(false);

        if (currentCoroutine == null)
            currentCoroutine = StartCoroutine(Initialize());
    }

    protected override IEnumerator Initialize()
    {
        MoneyManager._instance.Init();
        StoreManager._instance.LoadStoreElements();
        yield return StartCoroutine(TransitionManager._instance.FadeOutEffect());
        yield return new WaitForSeconds(0.5f);

        currentCoroutine = null;
    }

    #region Store
    public void LoadStore()
    {
        isOptionsOpen = !isOptionsOpen;
        if (currentCoroutine == null)
            currentCoroutine = StartCoroutine(Load_Store(isOptionsOpen));
    }

    IEnumerator Load_Store(bool _)
    {
        yield return StartCoroutine(TransitionManager._instance.FadeInEffect());
        // yield return new WaitForSeconds(0.5f);

        if (_)
        {
            for (float i = duration; i > 0; i -= Time.deltaTime)
            {
                optionsLayer.CanvasGroupFade(i);
                storeLayer.CanvasGroupFade(duration - i);
                yield return null;
            }

            optionsLayer.CanvasGroupInteractable(false);
            storeLayer.CanvasGroupInteractable(true);
        }
        else
        {
            for (float i = duration; i > 0; i -= Time.deltaTime)
            {
                storeLayer.CanvasGroupFade(i);
                optionsLayer.CanvasGroupFade(duration - i);
                yield return null;
            }

            storeLayer.CanvasGroupInteractable(false);
            optionsLayer.CanvasGroupInteractable(true);
        }

        yield return StartCoroutine(TransitionManager._instance.FadeOutEffect());

        currentCoroutine = null;
    }

    #endregion
}
