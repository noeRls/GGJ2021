using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Monster : MonoBehaviour
{
    public float visionRange = 10;
    public float visionAngle = 90;
    public float hearingRatio = 1;
    public float speedWalk = 1;
    public float speedRun = 2;

    private GameObject player;
    private PlayerStats playerStats;
    protected NavMeshAgent navMeshAgent;
    protected Vector3 target = new Vector3(0, 0, 0);
    protected Vector3? playerPos = null;

    public float getTargetDistance()
    {
        return (player.transform.position - transform.position).magnitude;
    }
    public bool haveTarget()
    {
        return target != getDefaultTarget();
    }

    public bool isRunning()
    {
        return haveTarget();
    }

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (!player)
        {
            print("Error: no player found");
        }
        playerStats = player.GetComponent<PlayerStats>();
    }

    Vector3 getDefaultTarget()
    {
        return new Vector3(0, 0, 0);
    }

    void updatePlayerPos()
    {
        Vector3 playerDirection = player.transform.position - transform.position;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, playerDirection, out hit, visionRange) &&
            hit.collider.gameObject.CompareTag("Player") &&
            Vector3.Angle(transform.forward, playerDirection.normalized) <= visionAngle / 2.0f
        )
        {
            playerPos = hit.point;
            return;
        }

        if (playerStats.getSoundDistance() * hearingRatio >= playerDirection.magnitude)
        {
            playerPos = player.transform.position;
        }
    }

    virtual protected void updateTarget()
    {
        if (playerPos.HasValue)
        {
            target = playerPos.Value;
        }
    }

    virtual protected void onTargetReach()
    {
        if (playerPos.HasValue && target == playerPos.Value)
        {
            playerPos = null;
        }
        target = getDefaultTarget();
    }

    void checkTargetDestination()
    {
        if ((target - transform.position).magnitude <= 1)
        {
            onTargetReach();
        }
    }

    virtual protected void setSpeed()
    {
        navMeshAgent.speed = isRunning() ? speedRun : speedWalk;
    }

    protected void monsterUpdate()
    {
        updatePlayerPos();
        updateTarget();
        checkTargetDestination();
        setSpeed();
        navMeshAgent.SetDestination(target);
    }

    // Update is called once per frame
    void Update()
    {
        monsterUpdate();
    }
}
