using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiManager : MonoBehaviour
{
    public Canvas hud;
    public Canvas shop;

    public ShopTrigger shopTrigger;
    public PlayerStats playerStats;

        // Start is called before the first frame update
    void Start()
    {
        ExitShop();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ExitShop()
    {
        playerStats.Unfreeze(); 
        hud.enabled = true;
        shop.enabled = false;
    }

    public void EnterShop()
    {
        playerStats.Freeze();
        hud.enabled = false;
        shop.enabled = true;
    }
}
