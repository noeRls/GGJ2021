using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum SpawnRate
{
    LOW = 1,
    MEDIUM = 2,
    HIGH = 3
}
[System.Serializable] public struct SpawnMobInfo
{
    public GameObject prefab;
    public SpawnRate rate;
    public bool isSpider;
}

public class MonsterSpawner : MonoBehaviour
{
    public float radius = 10f;
    public float increaseByRound = 0.5f;
    public int monsterSpawned = 10;
    public List<SpawnMobInfo> mobs;
    private GameManager gameManager;
    public bool spiderSpawner = false;

    SpawnMobInfo getRandomMonster()
    {
        int totalMonsterSize = 0;
        mobs.ForEach(m =>
        {
            totalMonsterSize += (int)m.rate;
        });
        int idxWithPriority = Random.Range(0, totalMonsterSize);
        foreach (SpawnMobInfo mob in mobs)
        {
            idxWithPriority -= (int)mob.rate;
            if (idxWithPriority < 0)
            {
                return mob;
            }
        }
        return mobs[mobs.Count - 1];
    }

    public static bool clipPointToNavMesh(Vector3 point, out Vector3 result, int agentId = 0)
    {
        NavMeshHit myNavHit;
        NavMeshQueryFilter queryFilter = new NavMeshQueryFilter();
        queryFilter.areaMask = -1;
        queryFilter.agentTypeID = agentId;
        if (NavMesh.SamplePosition(point, out myNavHit, 100, queryFilter))
        {
            result = myNavHit.position;
            return true;
        }
        else
        {
            Debug.LogWarning("Failed to generate a good point direction for monster");
            result = new Vector3(0, 0, 0);
            return false;
        }
    }
    Vector3 getRandomSpawnPosition(int agentTypeId)
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 randomPointInRange = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)) * radius;
            Vector3 worldPos = transform.position + randomPointInRange;
            Vector3 goodPos;
            if (clipPointToNavMesh(worldPos, out goodPos, agentTypeId))
            {
                return goodPos;
            }
        }
        Debug.LogWarning("Failed to generate a good spawn position for monster");
        return new Vector3(0, 0, 0);
    }

    public void spawnMonsters()
    {
        float ratioBonus = gameManager.days - 1 * increaseByRound;
        for (int i = 0; i < monsterSpawned * (1 + ratioBonus); i++)
        {
            SpawnMobInfo mob = getRandomMonster();
            Vector3 spawnPos = getRandomSpawnPosition(mob.isSpider ? 1 : 0);
            Instantiate(mob.prefab, spawnPos, Quaternion.Euler(0, Random.Range(0f, 380f), 0));
        }
    }

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        if (spiderSpawner)
        {
            spawnMonsters();
        } else
        {
            gameManager.onNightStart += spawnMonsters;
        }
    }
}
