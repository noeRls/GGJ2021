using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemsControl : MonoBehaviour
{
    private ItemList itemLists;
    private TrapPreview preview = null;
    // Start is called before the first frame update
    void Start()
    {
        itemLists = GameObject.FindGameObjectWithTag("Database").GetComponent<ItemList>();
    }

    void placeTrap(ItemInfo item)
    {
        print("Creating preview");
        preview = Instantiate(item.prefab,
            transform.position + transform.forward.normalized * 5,
            transform.rotation,
            transform
        ).GetComponent<TrapPreview>();
    }

    void useDrug(ItemInfo item)
    {

    }

    void removePreviewIfAny()
    {
        if (preview != null)
        {
            preview.cancel();
            preview = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (ItemInfo itemInfo in itemLists.items)
        {
            if (Input.GetKeyDown(itemInfo.activationKey))
            {
                print("HOHO");
                removePreviewIfAny();
                if (itemInfo.category == ItemCategory.TRAP)
                {
                    placeTrap(itemInfo);
                } else
                {
                    useDrug(itemInfo);
                }
            }
        }

        if (Input.GetButton("Fire1"))
        {
            if (preview != null)
            {
                // TODO remove money
                preview.confirm();
                preview = null;
            }
        }
        else if (Input.GetButton("Fire2"))
        {
            removePreviewIfAny();
        }
    }
}
