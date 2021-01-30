using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotor : MonoBehaviour
{
    public Animator animator;
    private Monster monster;
    private MonsterAttackPunch attack;

    // Start is called before the first frame update
    void Start()
    {
        attack = GetComponent<MonsterAttackPunch>();
        attack.onAttack += onAttack;
        monster = GetComponent<Monster>();
    }

    void onAttack()
    {
        animator.SetTrigger("attack");
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isRunning", monster.isRunning());
    }
}
