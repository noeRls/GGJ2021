using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiManager : MonoBehaviour
{
    public Canvas hud;
    public Canvas shop;

    public ShopTrigger shopTrigger;

        // Start is called before the first frame update
    void Start()
    {
        hud.enabled = true;
        shop.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (shopTrigger.isPlayerIn)
        {
            hud.enabled = false;
            shop.enabled = true;
        }
        else
        {
            hud.enabled = true;
            shop.enabled = false;
        }
    }
}
