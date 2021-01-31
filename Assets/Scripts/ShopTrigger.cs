using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    public Collider player;
    
    public bool isPlayerIn = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider shouldBePlayer)
    {
        if (shouldBePlayer != player)
            return;

        isPlayerIn = true;
    }

    private void OnTriggerExit(Collider shouldBePlayer)
    {
        if (shouldBePlayer != player)
            return;

        isPlayerIn = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
