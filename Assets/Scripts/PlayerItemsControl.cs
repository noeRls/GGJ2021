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
    private ItemList itemLists;
    private PreviewInfo? preview = null;
    private PlayerStats stats;
    // Start is called before the first frame update
    void Start()
    {
        itemLists = GameObject.FindGameObjectWithTag("Database").GetComponent<ItemList>();
        stats = GetComponent<PlayerStats>();
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
                (stats.inventory[itemInfo.itemType] > 0 || unlimited)
            )
            {
                removePreviewIfAny();
                if (itemInfo.category == ItemCategory.TRAP)
                {
                    previewTrap(itemInfo);
                } else
                {
                    stats.inventory[itemInfo.itemType] -= 1;
                    useDrug(itemInfo);
                }
            }
        }

        if (Input.GetButton("Fire1"))
        {
            if (preview != null)
            {
                stats.inventory[preview.Value.item.itemType] -= 1;
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
