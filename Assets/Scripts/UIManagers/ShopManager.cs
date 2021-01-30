using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class ShopManager : MonoBehaviour
{
    public Button exitButton, buyButton;

    public Transform scrollViewItemContent;
    public GameObject prefabItemGridElement;

    public ItemList database;
    public GuiManager guiManager;

    // Start is called before the first frame update
    void Start()
    {
        foreach(ItemInfo item in database.items)
        {
            GameObject createdGridElement = Instantiate(prefabItemGridElement);
            ItemGridElementInfo createdGridElementInfo = createdGridElement.GetComponent<ItemGridElementInfo>();

            createdGridElementInfo.itemName.text = item.name;
            createdGridElementInfo.itemDescription.text = item.description;
            createdGridElementInfo.itemInfo = item;
            
            createdGridElement.transform.SetParent(scrollViewItemContent, false);
            createdGridElement.transform.localScale = Vector3.one;
        }

        exitButton.onClick.AddListener(ExitShop);
    }

    private void ExitShop()
    {
        guiManager.ExitShop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(eventData);
    }
}
