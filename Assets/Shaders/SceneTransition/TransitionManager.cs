using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager _instance;

    [SerializeField] AnimationCurve animationCurve;

    [SerializeField] Material material;
    [SerializeField] float duration = 1f;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this.gameObject);
    }

    public IEnumerator FadeOutEffect()
    {
        material.SetFloat("_Cutoff", 1);

        for (float i = duration; i > 0; i -= Time.deltaTime)
        {
            material.SetFloat("_Cutoff", animationCurve.Evaluate(i / duration));
            yield return null;
        }
        material.SetFloat("_Cutoff", 0);
        yield return null;

    }

    public IEnumerator FadeInEffect()
    {
        material.SetFloat("_Cutoff", 0);

        for (float i = 0; i < duration; i += Time.deltaTime)
        {
            material.SetFloat("_Cutoff", animationCurve.Evaluate(i / duration));
            yield return null;
        }
        material.SetFloat("_Cutoff", 1);
        yield return null;

    }
}