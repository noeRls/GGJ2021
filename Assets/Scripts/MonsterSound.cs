using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSound : MonoBehaviour
{
    private AudioSource MonsterSource;
    public AudioClip WalkMonster;
    public AudioClip RunMonster;
    private float SoundTimer = 10;
    private Monster stat;

    // Start is called before the first frame update
    // The sound of each monster is set to the marching sound
    private void Start()
    {
        stat = GetComponentInParent<Monster>();
        MonsterSource = GetComponent<AudioSource>();
        if (!WalkMonster || !RunMonster)
        {
            Debug.LogWarning("Missing sound for monster");
        }
    }

    // Update is called once per frame
    void Update()
    {
        SoundTimer -= Time.deltaTime;
        if (SoundTimer <= 0)
        {
            //Check if monster is running
            bool Run = stat.isRunning();
            if (Run)
            {
                MonsterSource.clip = RunMonster;
            }
            else
            {
                MonsterSource.clip = WalkMonster;
            }
            MonsterSource.Play();
            SoundTimer = 10;
        }
    }
}
