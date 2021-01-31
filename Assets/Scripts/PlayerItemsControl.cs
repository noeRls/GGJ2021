using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct PreviewInfo
{
    public ItemInfo item;
    public TrapPreview preview;
}

public class PlayerItemsControl : MonoBehaviour
{
    public bool unlimited = false;
    public Dictionary<ItemType, int> inventory = new Dictionary<ItemType, int>();
    private ItemList itemLists;
    private PreviewInfo? preview = null;
    private PlayerStats stats;
    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<PlayerStats>();
        itemLists = GameObject.FindGameObjectWithTag("Database").GetComponent<ItemList>();
        foreach (var item in itemLists.items)
        {
            inventory[item.itemType] = 0;
        }
    }

    void previewTrap(ItemInfo item)
    {
        GameObject previewObject = Instantiate(item.prefab,
            transform.position + transform.forward.normalized * 5,
            transform.rotation,
            transform
        );
        preview = new PreviewInfo { preview = previewObject.GetComponent<TrapPreview>(), item = item };
    }

    void useDrug(ItemInfo item)
    {
        switch (item.itemType)
        {
            case ItemType.HEALTHPACK:
                stats.useHealthPack();
                break;
            case ItemType.RUNNER_POTION:
                stats.useRunnerPotion();
                break;
        }
    }

    void removePreviewIfAny()
    {
        if (preview != null)
        {
            preview.Value.preview.cancel();
            preview = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (ItemInfo itemInfo in itemLists.items)
        {
            if (Input.GetKeyDown(itemInfo.activationKey) &&
                (inventory[itemInfo.itemType] > 0 || unlimited)
            )
            {
                removePreviewIfAny();
                if (itemInfo.category == ItemCategory.TRAP)
                {
                    previewTrap(itemInfo);
                } else
                {
                    inventory[itemInfo.itemType] -= 1;
                    useDrug(itemInfo);
                }
            }
        }

        if (Input.GetButton("Fire1"))
        {
            if (preview != null)
            {
                // TODO remove money
                inventory[preview.Value.item.itemType] -= 1;
                preview.Value.preview.confirm();
                preview = null;
            }
        }
        else if (Input.GetButton("Fire2"))
        {
            removePreviewIfAny();
        }
    }
}
