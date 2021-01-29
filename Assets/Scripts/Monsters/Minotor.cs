using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotor : MonoBehaviour
{
    public Animator animator;
    private Monster monster;

    // Start is called before the first frame update
    void Start()
    {
        monster = GetComponent<Monster>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isRunning", monster.isRunning());
    }
}
