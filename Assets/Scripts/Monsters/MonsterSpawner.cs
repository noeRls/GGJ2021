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
}

public class MonsterSpawner : MonoBehaviour
{
    public float radius = 10f;
    public int monsterSpawned = 10;
    public List<SpawnMobInfo> mobs;

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

    Vector3 getRandomSpawnPosition()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 randomPointInRange = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)) * radius;
            Vector3 worldPos = transform.position + randomPointInRange;
            NavMeshHit myNavHit;
            if (NavMesh.SamplePosition(worldPos, out myNavHit, 100, -1))
            {
                return myNavHit.position;
            }
        }
        Debug.LogWarning("Failed to generate a good spawn position for monster");
        return new Vector3(0, 0, 0);
    }

    public void spawnMonsters()
    {
        for (int i = 0; i < monsterSpawned; i++)
        {
            SpawnMobInfo mob = getRandomMonster();
            Vector3 spawnPos = getRandomSpawnPosition();
            Instantiate(mob.prefab, spawnPos, Quaternion.Euler(0, Random.Range(0f, 380f), 0));
        }
    }

    private void Start()
    {
        spawnMonsters();
    }
}
