using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public float visionRange = 10;
    public float visionAngle = 90;
    public float hearingRatio = 1;
    public float speedWalk = 1;
    public float speedRun = 2;
    private float refreshTimeout = 0;

    private GameObject player;
    private PlayerStats playerStats;
    protected NavMeshAgent navMeshAgent;
    public Vector3 target = new Vector3(0, 0, 0);
    protected Vector3? playerPos = null;
    private bool isTrapped = false;
    private GameManager manager;

    private Vector3 mapCenter = new Vector3(500, 122, 500);
    public Vector3 defaultTarget;

    private void Awake()
    {
        NavMesh.pathfindingIterationsPerFrame = 500;
    }

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
        updateDefaultTarget();
        target = getDefaultTarget();
        GameManager manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        manager.onDayStart += onDayStart;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    void onDayStart()
    {
        if (!isTrapped)
        {
            Destroy();
        }
    }

    Vector3 getDefaultTarget()
    {
        return defaultTarget;
    }

    Vector3 getOppositePosition()
    {
        Vector3 diffFromCenter = transform.position - mapCenter;
        Vector3 opposedDirection = transform.position - diffFromCenter * 2;
        print(transform.position);
        opposedDirection.y = transform.position.y;
        print(opposedDirection);
        return opposedDirection;
    }

    Vector3 getCloseRandomPosition()
    {
        return transform.position + new Vector3(Random.Range(0.0f, 10f), 0, Random.Range(0.0f, 10f));
    }

    void updateDefaultTarget()
    {
        Vector3 position = getCloseRandomPosition();
        Vector3 goodPosition;
        if (!MonsterSpawner.clipPointToNavMesh(position, out goodPosition))
        {
            Debug.LogWarning("Failed to generate default target for monster");
        }
        defaultTarget = goodPosition;
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
        if (target == getDefaultTarget())
        {
            updateDefaultTarget();
        }
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

    public void getTrapped()
    {
        isTrapped = true;
        navMeshAgent.isStopped = true;
    }

    protected void monsterUpdate()
    {
        if (isTrapped) return;
        Vector3 lastTarget = target;
        updatePlayerPos();
        updateTarget();
        checkTargetDestination();
        setSpeed();
        refreshTimeout -= Time.deltaTime;
        if (target != lastTarget && refreshTimeout < 0)
        {
            refreshTimeout = 0.1f;
            navMeshAgent.SetDestination(target);
        }
    }

    // Update is called once per frame
    void Update()
    {
        monsterUpdate();
    }
}
