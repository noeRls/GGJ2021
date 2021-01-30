using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float hp = 100.0f;
    public float endurance = 100f;
    public int money = 666;

    public float walkSpeed = 6f;
    public float runSpeed = 10f;
    public float jumpSpeed = 8f;

    public float soundDistanceRunning = 20f;
    public float soundDistanceWalking = 10f;

    public bool canRun = true;
    public bool canMove = true;

    public bool running = false;
    public bool moving = false;
    public bool dead = false;

    public float getSoundDistance()
    {
        if (!moving) return 0;
        return running ? soundDistanceRunning : soundDistanceWalking;
    }

    public void doDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            dead = true;
            canMove = false;
        }
    }
}
