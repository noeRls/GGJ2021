using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float hp = 100.0f;
    public float walkSpeed = 6f;
    public float runSpeed = 10f;
    public float jumpSpeed = 8f;
    public float endurance = 100f;

    public float soundDistanceRunning = 20f;
    public float soundDistanceWalking = 10f;

    public bool running = false;
    public bool moving = false;
    public float getSoundDistance()
    {
        if (!moving) return 0;
        return running ? soundDistanceRunning : soundDistanceWalking;
    }

    public void doDamage(float damage)
    {
        hp -= damage;
        if (hp < 0)
        {
            print("Plyaer died");
        }
    }
}
