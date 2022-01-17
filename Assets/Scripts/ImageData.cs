using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum StoreElementType
{
    Emerald,
    Item,
    Chest
}

public class ImageData : MonoBehaviour
{
    public StoreElementType type;
    [SerializeField] int id;
    [SerializeField] TextMeshProUGUI item_titleText;
    [SerializeField] TextMeshProUGUI item_buttonText;
    [SerializeField] Image item_image;

    [SerializeField] int emeraldCost;
    [SerializeField] int goldCost;
    [SerializeField] int dustCost;

    [SerializeField] GameObject emeraldContainer;


    [SerializeField] Button btn;

    public void Init(bool _isActive, string _item_title, int _item_button, Sprite _item_sprite)
    {
        string moneySign = "";
        switch (type)
        {
            case StoreElementType.Emerald:
                emeraldContainer.SetActive(false);
                moneySign = "S./ ";
                goldCost = _item_button;
                break;
            case StoreElementType.Item:
                emeraldContainer.SetActive(true);
                moneySign = " ";
                emeraldCost = _item_button;
                break;
            case StoreElementType.Chest:
                emeraldContainer.SetActive(false);
                moneySign = "S./ ";
                dustCost = _item_button;
                break;
        }
        btn.interactable = _isActive;
        item_titleText.text = _item_title;
        item_image.sprite = _item_sprite;
        item_buttonText.text = moneySign + _item_button;
    }

    public void BuyItem()
    {
        switch (type)
        {
            case StoreElementType.Emerald:
                if (goldCost <= SaveManager._instance.saveData.money_gold)
                {
                    SaveManager._instance.UseGold(goldCost);
                    SaveManager._instance.BuyItem_Emeralds(id);
                    btn.interactable = false;
                }
                break;
            case StoreElementType.Item:
                if (emeraldCost <= SaveManager._instance.saveData.money_emeralds)
                {
                    SaveManager._instance.UseEmeralds(emeraldCost);
                    SaveManager._instance.BuyItem_Items(id);
                    btn.interactable = false;
                }
                break;
        }
    }
}
