using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    public GuiManager guiManager;

    private void OnTriggerEnter(Collider shouldBePlayer)
    {
        if (!shouldBePlayer.gameObject.CompareTag("Player"))
            return;

        guiManager.EnterShop();
    }

    private void OnTriggerExit(Collider shouldBePlayer)
    {
        if (!shouldBePlayer.gameObject.CompareTag("Player"))
            return;

        guiManager.ExitShop();
    }
}
