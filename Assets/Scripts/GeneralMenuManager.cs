using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralMenuManager : MenuManager_Parent
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Init()
    {
        if (currentCoroutine == null)
            currentCoroutine = StartCoroutine(Initialize());
    }

    protected override IEnumerator Initialize()
    {
        MoneyManager._instance.Init();
        yield return StartCoroutine(TransitionManager._instance.FadeOutEffect());
        yield return new WaitForSeconds(0.5f);

        currentCoroutine = null;
    }
}
