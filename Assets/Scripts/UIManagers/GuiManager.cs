using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour
{
    public Canvas hud;
    public Canvas shop;
    public bool isShopInteractable = false;

    public StateScreenManager stateScreen;

    public ShopTrigger shopTrigger;
    private PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject
            .FindGameObjectWithTag("Player")
            .GetComponent<PlayerStats>();

        hud.enabled = true;
        shop.enabled = false;
        stateScreen.Summon(StateScreenManager.State.OFF);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (shop.enabled)
                ExitShop();
            else
                TogglePause();
        }
    }

    public void TogglePause()
    {
        bool isPauseEnabled = stateScreen.CurrentState() == StateScreenManager.State.WIN;
        stateScreen.Summon(isPauseEnabled ? StateScreenManager.State.OFF : StateScreenManager.State.WIN);
        hud.enabled = isPauseEnabled;
    }

    public void ExitShop()
    {
        playerStats.Unfreeze(); 
        hud.enabled = true;
        shop.enabled = false;
        isShopInteractable = false;
    }

    public void EnterShop()
    {
        playerStats.Freeze();
        hud.enabled = false;
        shop.enabled = true;
        isShopInteractable = true;
    }
}
