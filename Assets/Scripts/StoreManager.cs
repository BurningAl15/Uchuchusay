using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class StoreElementData
{
    public string item_title;
    public int item_button;
    public Sprite item_sprite;
    // public bool isActive;
    public ImageData imageData;

    public void Init(bool _)
    {
        imageData.Init(_, item_title, item_button, item_sprite);
    }
}

public class StoreManager : MonoBehaviour
{
    public static StoreManager _instance;

    public StoreElementType type;

    [Header("Emerald")]
    [SerializeField] GameObject emeraldContainer;
    [SerializeField] Image emeraldImage;
    [SerializeField] List<StoreElementData> storeElementDataList = new List<StoreElementData>();

    [Header("Items")]
    [SerializeField] GameObject itemContainer;
    [SerializeField] Image itemImage;
    [SerializeField] List<StoreElementData> storeElement_Item_List = new List<StoreElementData>();

    [Header("Chest")]
    [SerializeField] GameObject chestContainer;
    [SerializeField] Image chestImage;
    [SerializeField] List<StoreElementData> storeElement_Chest_List = new List<StoreElementData>();

    [Header("Sprites")]

    [SerializeField] Sprite normal;
    [SerializeField] Sprite highlight;

    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this.gameObject);
    }

    public void LoadStoreElements()
    {
        SwitchData();
    }

    public void LoadStoreElements(int _storeType)
    {
        type = (StoreElementType)_storeType;
        SwitchData();
    }

    void SwitchData()
    {
        switch (type)
        {
            case StoreElementType.Emerald:
                UpdateContainers();

                for (int i = 0; i < storeElementDataList.Count; i++)
                    storeElementDataList[i].Init(!SaveManager._instance.saveData.boughtItems_emeralds[i]);
                break;
            case StoreElementType.Item:
                UpdateContainers();

                for (int i = 0; i < storeElement_Item_List.Count; i++)
                    storeElement_Item_List[i].Init(!SaveManager._instance.saveData.boughtItems_items[i]);
                break;
            case StoreElementType.Chest:
                UpdateContainers();

                for (int i = 0; i < storeElement_Chest_List.Count; i++)
                    storeElement_Chest_List[i].Init(!SaveManager._instance.saveData.boughtItems_chests[i]);
                break;
        }
    }

    void UpdateContainers()
    {
        switch (type)
        {
            case StoreElementType.Emerald:
                emeraldContainer.SetActive(true);
                itemContainer.SetActive(false);
                chestContainer.SetActive(false);

                emeraldImage.sprite = highlight;
                itemImage.sprite = normal;
                chestImage.sprite = normal;
                break;
            case StoreElementType.Item:
                emeraldContainer.SetActive(false);
                itemContainer.SetActive(true);
                chestContainer.SetActive(false);

                emeraldImage.sprite = normal;
                itemImage.sprite = highlight;
                chestImage.sprite = normal;

                break;
            case StoreElementType.Chest:
                emeraldContainer.SetActive(false);
                itemContainer.SetActive(false);
                chestContainer.SetActive(true);

                emeraldImage.sprite = normal;
                itemImage.sprite = normal;
                chestImage.sprite = highlight;
                break;
        }
    }
}
