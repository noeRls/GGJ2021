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
    private Vector3 originalPoint;
    public float runAwaySpeed = 5f;
    public float comeBackSpeed = 2f;
    public float comeBackTimeout = 5f;
    private float comeBackTimer = 0f;
    private bool runningAway = false;

    // Start is called before the first frame update
    void Start()
    {
        originalPoint = transform.position;
        spiderAnimation = GetComponentInChildren<AnimationsController>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distanceToPlayer = transform.position - player.transform.position;
        if (distanceToPlayer.magnitude > 100) { return; }
        if (distanceToPlayer.magnitude < playerStats.getSoundDistance())
        {
            Vector3 targetPoint = transform.position + (distanceToPlayer.normalized * playerStats.getSoundDistance());
            NavMeshHit myNavHit;
            if (NavMesh.SamplePosition(targetPoint, out myNavHit, 100, -1))
            {
                targetPoint = myNavHit.position;
            }
            runningAway = true;
            agent.speed = runAwaySpeed;
            spiderAnimation.SetMovingState(true);
            agent.SetDestination(targetPoint);
        }
        comeBackTimer -= Time.deltaTime;
        if ((agent.destination - transform.position).magnitude < 1)
        {
            if (runningAway)
            {
                runningAway = false;
                spiderAnimation.SetMovingState(false);
                comeBackTimer = comeBackTimeout;
            } else if (comeBackTimer < 0)
            {
                agent.SetDestination(originalPoint);
                agent.speed = comeBackSpeed;
            }
        }
    }
}
