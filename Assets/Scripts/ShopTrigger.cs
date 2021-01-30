using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    public Collider player;
    public PlayerStats stats;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider shouldBePlayer)
    {
        if (shouldBePlayer != player)
            return;

        stats.doDamage(50);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
