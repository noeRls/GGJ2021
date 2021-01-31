using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiManager : MonoBehaviour
{
    public Canvas hud;
    public Canvas shop;
    public Canvas pauseMenu;

    private Canvas currentlyDisplayedCanvas;

    public ShopTrigger shopTrigger;
    private PlayerStats playerStats;

        // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject
            .FindGameObjectWithTag("Player")
            .GetComponent<PlayerStats>();
        ExitShop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
            TogglePause();
    }

    public void TogglePause()
    {
        pauseMenu.enabled = !pauseMenu.enabled;
        Time.timeScale = pauseMenu.enabled ? 1f : 0f;
        print(pauseMenu.enabled);
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
