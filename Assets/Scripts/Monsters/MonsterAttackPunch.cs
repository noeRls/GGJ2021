using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MonsterAttackPunch: MonoBehaviour
{
    public float punchInterval = 1;
    public float punchDamage = 20;
    public Action onAttack;

    private Monster monster;
    private float punchTimeout = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        onAttack += () => { };
        monster = GetComponent<Monster>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") &&
            punchTimeout < 0)
        {
            PlayerStats stats = other.GetComponent<Collider>().gameObject.GetComponent<PlayerStats>();
            stats.doDamage(punchDamage);
            punchTimeout = punchInterval;
            onAttack();
        }
    }

    // Update is called once per frame
    void Update()
    {
        punchTimeout -= Time.deltaTime;
    }
}
