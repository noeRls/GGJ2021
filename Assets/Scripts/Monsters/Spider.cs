using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spider : MonoBehaviour
{
    private GameObject player;
    private PlayerStats playerStats;
    private NavMeshAgent agent;
    private AnimationsController spiderAnimation;

    // Start is called before the first frame update
    void Start()
    {
        spiderAnimation = GetComponentInChildren<AnimationsController>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 distanceToPlayer = transform.position - player.transform.position;
        if (distanceToPlayer.magnitude < playerStats.getSoundDistance())
        {
            Vector3 targetPoint = transform.position + (distanceToPlayer.normalized * playerStats.getSoundDistance());
            NavMeshHit myNavHit;
            if (NavMesh.SamplePosition(targetPoint, out myNavHit, 100, -1))
            {
                targetPoint = myNavHit.position;
            }
            spiderAnimation.SetMovingState(true);
            agent.SetDestination(targetPoint);
        }
        if ((agent.destination - transform.position).magnitude < 1)
        {
            spiderAnimation.SetMovingState(false);
        }
    }
}
