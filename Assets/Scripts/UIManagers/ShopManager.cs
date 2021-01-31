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

    ItemInfo currentlySelected;

    public GuiManager guiManager;
    private ItemList database;
    private PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        database = GameObject.FindGameObjectWithTag("Database").GetComponent<ItemList>();

        moneyDisplay.text = $"{playerStats.money} €";

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener(data => { OnItemGridElementClick((PointerEventData)data); });

        foreach(ItemInfo item in database.items)
        {
            GameObject createdGridElement = Instantiate(prefabItemGridElement);
            ItemGridElementInfo createdGridElementInfo = createdGridElement.GetComponent<ItemGridElementInfo>();

            createdGridElementInfo.itemName.text = item.name;
            createdGridElementInfo.itemInfo = item;

            EventTrigger trigger = createdGridElement.GetComponent<EventTrigger>();
            trigger.triggers.Add(entry);

            createdGridElement.transform.localScale = Vector3.one;
            createdGridElement.SetActive(true);
            createdGridElement.transform.SetParent(scrollViewItemContent, false);
        }

        SelectItem(database.items[0]);
        exitButton.onClick.AddListener(ExitShop);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnItemGridElementClick(PointerEventData eventData)
    {
        SelectItem(eventData.pointerPress.GetComponent<ItemGridElementInfo>().itemInfo);
    }

    private void SelectItem(ItemInfo item)
    {
        currentlySelected = item;
        description.text = currentlySelected.description;
        buyButton.GetComponentInChildren<Text>().text = $"Buy ({currentlySelected.price} €)";

        buyButton.interactable = currentlySelected.price < playerStats.money;
    }

    private void ExitShop()
    {
        guiManager.ExitShop();
    }
}
