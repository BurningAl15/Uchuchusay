using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager _instance;
    [SerializeField] TextMeshProUGUI emeraldText;
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI dustText;

    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this.gameObject);
    }

    public void Init()
    {
        if (emeraldText != null)
        {
            emeraldText.text = "" + SaveManager._instance.saveData.money_emeralds;
            goldText.text = "" + SaveManager._instance.saveData.money_gold;
            dustText.text = "" + SaveManager._instance.saveData.money_dust;
        }
    }
}
