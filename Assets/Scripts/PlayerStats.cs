using System.Collections.Generic;
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

    public bool lockMouse = true;

    private float runnerPotionTimeRemaining = 0.0f;

    private Dictionary<ItemType, int> inventory = new Dictionary<ItemType, int>();
    public float getSoundDistance()
    {
        if (!moving) return 0;
        return running ? soundDistanceRunning : soundDistanceWalking;
    }

    void Start()
    {
        Inventory();
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

    public void Freeze()
    {
        canMove = false;
        lockMouse = false;
    }

    public void Unfreeze()
    {
        canMove = true;
        lockMouse = true;
    }

    public void BuyItem(ItemInfo item)
    {
        money -= item.price;
        inventory[item.itemType] += 1;
    }

    public void useHealthPack()
    {
        hp = Mathf.Min(100f, hp + 50);
    }

    public void useRunnerPotion()
    {
        runnerPotionTimeRemaining = 60 * 2;
    }

    private void Update()
    {
        runnerPotionTimeRemaining -= Time.deltaTime;
    }

    public float getWalkSpeed()
    {
        return runnerPotionTimeRemaining > 0 ? walkSpeed * 2 : walkSpeed;
    }
    public float getRunSpeed()
    {
        return runnerPotionTimeRemaining > 0 ? runSpeed * 2 : runSpeed;
    }

    public Dictionary<ItemType, int> Inventory()
    {
        if (inventory.Count == 0)
        {
            foreach (var item in GameObject.FindGameObjectWithTag("Database").GetComponent<ItemList>().items)
            {
                inventory[item.itemType] = 0;
            }
        }
        return inventory;
    }
}
