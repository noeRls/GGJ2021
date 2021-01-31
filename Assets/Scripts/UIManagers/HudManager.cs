using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    private PlayerStats playerStats;

    public Slider hpDisplay;
    public Slider enduranceDisplay;

    public Text stateDisplay;
    public Text moneyDisplay;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        moneyDisplay.text = $"{playerStats.money} €";
        hpDisplay.value = playerStats.hp;
        enduranceDisplay.value = playerStats.endurance;

        if (playerStats.dead)
        {
            stateDisplay.text = "You died !";
        }
    }
}
