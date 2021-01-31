using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    private PlayerStats playerStats;
    private ItemList database;

    public Transform scrollViewPossessionContent;
    public GameObject prefabItemListElement;

    public Slider hpDisplay;
    public Slider enduranceDisplay;
    public Text pickupHint;

    public Dictionary<ItemType, PossessionListItemInfo> tamer = new Dictionary<ItemType, PossessionListItemInfo>();

    // Start is called before the first frame update
    void Start()
    {
        pickupHint.gameObject.SetActive(false);
        playerStats = GameObject
            .FindGameObjectWithTag("Player")
            .GetComponent<PlayerStats>();

        database = GameObject
            .FindGameObjectWithTag("Database")
            .GetComponent<ItemList>();

        foreach (ItemInfo item in database.items)
        {
            GameObject createdListElement = Instantiate(prefabItemListElement);
            PossessionListItemInfo createdListElementInfo = createdListElement.GetComponent<PossessionListItemInfo>();

            createdListElementInfo.qte.text = $"{playerStats.Inventory()[item.itemType]}";
            createdListElementInfo.icon.sprite = item.icon;
            createdListElementInfo.itemInfo = item;
            tamer.Add(item.itemType, createdListElementInfo);

            createdListElement.transform.localScale = Vector3.one;
            createdListElement.SetActive(true);
            createdListElement.transform.SetParent(scrollViewPossessionContent, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        hpDisplay.value = playerStats.hp;
        enduranceDisplay.value = playerStats.endurance;

        foreach (ItemType item in tamer.Keys)
        {
            tamer[item].qte.text = $"{playerStats.Inventory()[item]}";
        }
    }

    public void TogglePickupHint(bool isActivated)
    {
        pickupHint.gameObject.SetActive(isActivated);
    }
}
