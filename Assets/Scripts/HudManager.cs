using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    public PlayerStats playerStats;

    public Text moneyDisplay;
    public Slider hpDisplay;
    public Slider enduranceDisplay;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        moneyDisplay.text = $"{playerStats.money} €";
        hpDisplay.value = playerStats.hp;
        enduranceDisplay.value = playerStats.endurance;
    }
}
