using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    private HudManager hudManager;

    private void Start()
    {
        hudManager = GameObject.FindGameObjectWithTag("HudManager").GetComponent<HudManager>();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hudManager.TogglePickupHint(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hudManager.TogglePickupHint(false);
        }
    }
}
