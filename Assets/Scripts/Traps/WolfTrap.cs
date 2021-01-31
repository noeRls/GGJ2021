using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfTrap : MonoBehaviour
{
    private bool isClose = false;
    public Animator animator;
    private GameObject player;
    private GameObject trappedMonster;

    public float harvestRange = 5;
    public int moneyByHarvest = 100;

    private HudManager hudManager;
    private bool tooltipStateInternal = false;


    // Update is called once per frame

    private void Start()
    {
        hudManager = GameObject.FindGameObjectWithTag("HudManager").GetComponent<HudManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void open()
    {
        isClose = false;
        animator.SetBool("closed", false);
        if (trappedMonster)
        {
            PlayerStats stats = player.GetComponent<PlayerStats>();
            stats.money += moneyByHarvest;
            trappedMonster.GetComponent<Monster>().Destroy();
            trappedMonster = null;
        }
    }

    void close()
    {
        isClose = true;
        animator.SetBool("closed", true);
    }

    private void Update()
    {
        if (isClose && (player.transform.position - transform.position).magnitude < harvestRange)
        {
            tooltipStateInternal = true;
            hudManager.TogglePickupHint(true);
            if (Input.GetKeyDown(KeyCode.E)) {
                open();
            }
        } else {
                    if (tooltipStateInternal)
            {
                hudManager.TogglePickupHint(false);
                tooltipStateInternal = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isClose) return;
        Monster monster = other.gameObject.GetComponent<Monster>();
        if (monster != null)
        {
            trappedMonster = other.gameObject;
            monster.getTrapped();
            close();
        }
    }
}
