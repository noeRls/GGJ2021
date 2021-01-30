using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class ShopManager : MonoBehaviour, IPointerClickHandler
{
    public Button exitButton, buyButton;
    public Transform baseSpawnPoint;
    public GameObject prefabItemGridElement;
    private RectTransform content;

    public ItemList database;

    // Start is called before the first frame update
    void Start()
    {
        int number = database.items.Count;

        for(int i = 0; i < number; i++)
        {
            float shiftX = i * 150;
            Vector3 spawnPosition = new Vector3(baseSpawnPoint.position.x + shiftX, baseSpawnPoint.position.y, baseSpawnPoint.position.z);
            GameObject gridElement = Instantiate(prefabItemGridElement, spawnPosition, baseSpawnPoint.rotation);
            gridElement.transform.SetParent(baseSpawnPoint, false);
            ItemGridElementInfo infos = gridElement.GetComponent<ItemGridElementInfo>();
            Debug.Log(infos);
            Debug.Log(infos.itemDescription.text);

            infos.itemDescription.text = database.items[i].description;
            infos.itemImage = null;
            infos.itemInfo = database.items[i];
        }

        exitButton.onClick.AddListener(ExitShop);


    }

    private void ExitShop()
    {

    }

    private void BuyItem(PointerEventData item)
    {

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
