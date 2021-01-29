using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float soundDistanceRunning = 20;
    public float soundDistanceWalking = 10;
    public bool running = false;
    public bool moving = true;
    public float getSoundDistance()
    {
        if (!moving) return 0;
        return running ? soundDistanceRunning : soundDistanceWalking;
    }
}
