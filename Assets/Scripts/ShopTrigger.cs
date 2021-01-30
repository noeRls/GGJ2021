using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    public Collider player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerControl>().playerController;
    }

    private void OnTriggerEnter(Collider shouldBePlayer)
    {
        if (shouldBePlayer != player)
            return;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
