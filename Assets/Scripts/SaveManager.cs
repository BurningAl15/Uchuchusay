using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public bool[] boughtItems_emeralds;
    public bool[] boughtItems_items;
    public bool[] boughtItems_chests;

    public int money_emeralds;
    public int money_gold;
    public int money_dust;
}

public class SaveManager : MonoBehaviour
{
    public static SaveManager _instance;

    public SaveData saveData;

    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this.gameObject);

        NewData();
    }

    #region Use
    public void UseEmeralds(int _cost)
    {
        saveData.money_emeralds -= _cost;
        Save();
    }
    public void UseGold(int _cost)
    {
        saveData.money_gold -= _cost;
        Save();
    }
    public void UseDust(int _cost)
    {
        saveData.money_dust -= _cost;
        Save();
    }
    #endregion

    #region Buy
    public void BuyEmeralds(int _cost)
    {
        saveData.money_emeralds += _cost;
        Save();
    }
    public void BuyGold(int _cost)
    {
        saveData.money_gold += _cost;
        Save();
    }
    public void BuyDust(int _cost)
    {
        saveData.money_dust += _cost;
        Save();
    }
    #endregion

    public void BuyItem_Items(int index)
    {
        saveData.boughtItems_items[index] = true;
        MoneyManager._instance.Init();
        Save();
    }

    public void BuyItem_Emeralds(int index)
    {
        saveData.boughtItems_emeralds[index] = true;
        MoneyManager._instance.Init();
        Save();
    }

    public void BuyItem_Chests(int index)
    {
        saveData.boughtItems_chests[index] = true;
        MoneyManager._instance.Init();
        Save();
    }

    public void Save()
    {
        string saveJson = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString("GameData", saveJson);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("GameData"))
        {
            string saveJson = PlayerPrefs.GetString("GameData");
            saveData = JsonUtility.FromJson<SaveData>(saveJson);
        }
        else
        {
            // !Data
            saveData.boughtItems_emeralds = new bool[3];
            saveData.boughtItems_items = new bool[3];
            saveData.boughtItems_chests = new bool[3];

            Save();
        }
    }

    public void NewData()
    {
        if (!PlayerPrefs.HasKey("InitialData"))
        {
            Load();

            // saveData.boughtItems
            saveData.money_emeralds = 100;
            saveData.money_gold = 100;
            saveData.money_dust = 100;

            Save();
            PlayerPrefs.SetString("InitialData", "true");
        }
        else
        {
            Load();
        }
    }
}
