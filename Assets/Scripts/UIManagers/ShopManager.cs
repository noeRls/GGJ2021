using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class ShopManager : MonoBehaviour
{
    public Button exitButton, buyButton;

    public Transform scrollViewItemContent;
    public GameObject prefabItemGridElement;

    public Text description;
    public Text moneyDisplay;

    ItemInfo currentlySelectedItem;

    public GuiManager guiManager;
    private ItemList database;
    private PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        database = GameObject.FindGameObjectWithTag("Database").GetComponent<ItemList>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener(data => { OnItemGridElementClick((PointerEventData)data); });

        foreach(ItemInfo item in database.items)
        {
            GameObject createdGridElement = Instantiate(prefabItemGridElement);
            ItemGridElementInfo createdGridElementInfo = createdGridElement.GetComponent<ItemGridElementInfo>();

            createdGridElementInfo.itemImage.sprite = item.icon;
            createdGridElementInfo.itemName.text = item.name;
            createdGridElementInfo.itemInfo = item;

            EventTrigger trigger = createdGridElement.GetComponent<EventTrigger>();
            trigger.triggers.Add(entry);

            createdGridElement.transform.localScale = Vector3.one;
            createdGridElement.SetActive(true);
            createdGridElement.transform.SetParent(scrollViewItemContent, false);
        }

        SelectItem(database.items[0]);
        buyButton.onClick.AddListener(BuyItem);
        exitButton.onClick.AddListener(ExitShop);
    }

    public void Update()
    {
        moneyDisplay.text = $"{playerStats.money} €";
    }

    public void BuyItem()
    {
        if (currentlySelectedItem.price < playerStats.money)
        {
            playerStats.BuyItem(currentlySelectedItem);
        }
        SelectItem(currentlySelectedItem);
    }

    public void OnItemGridElementClick(PointerEventData eventData)
    {
        SelectItem(eventData.pointerPress.GetComponent<ItemGridElementInfo>().itemInfo);
    }

    private void SelectItem(ItemInfo item)
    {
        currentlySelectedItem = item;
        description.text = currentlySelectedItem.description;
        buyButton.GetComponentInChildren<Text>().text = $"Buy ({currentlySelectedItem.price} €)";

        buyButton.interactable = currentlySelectedItem.price < playerStats.money;
    }

    private void ExitShop()
    {
        print("Exiting");
        guiManager.ExitShop();
    }
}
