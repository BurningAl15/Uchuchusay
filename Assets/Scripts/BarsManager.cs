using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Bar
{
    public float maxValue;
    public float currentValue;
    public Image barImage;
    public bool isActive;

    public void InitBar()
    {
        isActive = true;
        currentValue = maxValue;
        barImage.fillAmount = currentValue / maxValue;
    }

    public void UpdateBar(float _value)
    {
        currentValue = Mathf.Clamp(currentValue + _value, 0, maxValue);
        barImage.fillAmount = currentValue / maxValue;
        if (currentValue <= 0)
            isActive = false;
    }
}


public class BarsManager : MonoBehaviour
{
    public static BarsManager _instance;

    [SerializeField] Bar lifeBar;
    [SerializeField] Bar manaBar;


    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this.gameObject);

        lifeBar.InitBar();
        manaBar.InitBar();
    }

    public void DamageLife(float _damage)
    {
        lifeBar.UpdateBar(-_damage);
    }
    public void RecoverLife(float _recover)
    {
        lifeBar.UpdateBar(_recover);
    }

    public void UseMana(float _use)
    {
        manaBar.UpdateBar(-_use);
    }
    public void RecoverMana(float _recover)
    {
        manaBar.UpdateBar(_recover);
    }

    public bool IsAlive()
    {
        return lifeBar.isActive;
    }
}
