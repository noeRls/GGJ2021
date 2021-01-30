using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public float visionRange = 10;
    public float visionAngle = 90;
    public float hearingRatio = 1;
    public float speedWalk = 1;
    public float speedRun = 2;

    private GameObject player;
    private PlayerStats playerStats;
    private NavMeshAgent navMeshAgent;
    private Vector3 target = new Vector3(0, 0, 0);

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

    void updateTarget()
    {
        Vector3 playerDirection = player.transform.position - transform.position;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, playerDirection, out hit, visionRange) &&
            hit.collider.gameObject.CompareTag("Player") &&
            Vector3.Angle(transform.forward, playerDirection.normalized) <= visionAngle / 2.0f
        )
        {
            target = hit.point;
            return;
        }

        if (playerStats.getSoundDistance() * hearingRatio >= playerDirection.magnitude)
        {
            target = player.transform.position;
        }

        if ((target - transform.position).magnitude <= 0.1)
        {
            target = getDefaultTarget();
        }
    }

    // Update is called once per frame
    void Update()
    {
        updateTarget();
        navMeshAgent.speed = haveTarget() ? speedRun : speedWalk;
        navMeshAgent.SetDestination(target);
        // transform.position += (target - transform.position).normalized * Time.deltaTime;
    }
}
