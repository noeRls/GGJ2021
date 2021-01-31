using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour
{
    public Canvas hud;
    public Canvas shop;
    public bool isShopInteractable = false;

    public Canvas pauseMenu;
    private Button[] pauseMenuButtons;

    public ShopTrigger shopTrigger;
    private PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject
            .FindGameObjectWithTag("Player")
            .GetComponent<PlayerStats>();

        pauseMenuButtons = pauseMenu.GetComponentsInChildren<Button>();

        hud.enabled = true;
        shop.enabled = false;
        pauseMenu.enabled = false;
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
        pauseMenu.enabled = !pauseMenu.enabled;
        hud.enabled = !pauseMenu.enabled;
        Time.timeScale = pauseMenu.enabled ? 0f : 1f;

        foreach (Button b in pauseMenuButtons)
        {
            b.interactable = pauseMenu.enabled;
        }

        if (pauseMenu.enabled)
            playerStats.Freeze();
        else
            playerStats.Unfreeze();
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
