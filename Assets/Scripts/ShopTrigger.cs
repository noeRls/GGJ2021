using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    public GuiManager guiManager;
    public AmbientSound ambiantSound;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider shouldBePlayer)
    {
        if (!shouldBePlayer.gameObject.CompareTag("Player"))
            return;

        guiManager.EnterShop();
        ambiantSound.PlayShopAmbient();
    }

    private void OnTriggerExit(Collider shouldBePlayer)
    {
        if (!shouldBePlayer.gameObject.CompareTag("Player"))
            return;

        guiManager.ExitShop();
        ambiantSound.StopPlayingShopAmbiant();
    }
}
