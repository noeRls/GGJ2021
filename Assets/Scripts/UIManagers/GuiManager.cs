using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour
{
    public Canvas hud;
    public Canvas shop;

    public StateScreenManager stateScreen;

    public ShopTrigger shopTrigger;
    private PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject
            .FindGameObjectWithTag("Player")
            .GetComponent<PlayerStats>();

        hud.gameObject.SetActive(true);
        shop.gameObject.SetActive(false);
        stateScreen.Summon(StateScreenManager.State.OFF);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (shop.isActiveAndEnabled)
                ExitShop();
            else
                TogglePause();
        }

        if (playerStats.dead)
            BringDeath();
    }

    public void BringDeath()
    {
        hud.gameObject.SetActive(false);
        stateScreen.Summon(StateScreenManager.State.DEATH);
    }

    public void bringVictory()
    {
        hud.gameObject.SetActive(false);
        stateScreen.Summon(StateScreenManager.State.WIN);
    }

    public void TogglePause()
    {
        bool isPauseEnabled = stateScreen.CurrentState() == StateScreenManager.State.PAUSE;
        stateScreen.Summon(isPauseEnabled ? StateScreenManager.State.OFF : StateScreenManager.State.PAUSE);
        hud.gameObject.SetActive(isPauseEnabled);
    }

    public void ExitShop()
    {
        playerStats.Unfreeze();
        hud.gameObject.SetActive(true);
        shop.gameObject.SetActive(false);
    }

    public void EnterShop()
    {
        playerStats.Freeze();
        hud.gameObject.SetActive(false);
        shop.gameObject.SetActive(true);
    }
}
