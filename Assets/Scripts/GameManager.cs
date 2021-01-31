using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public GameObject sunLight;
    public Action onNightStart;
    public Action onDayStart;

    public float timeOfNight = 60 * 2;
    public float timeOfDay = 60;
    public float days = 1;

    private float timer = 0;
    public bool night = false;
    // Start is called before the first frame update
    void Start()
    {
        onNightStart += () => { };
        onDayStart += () => { };
        timer = timeOfDay / 2.0f;
    }

    void Update()
    {
        float rotation = Time.deltaTime * 180 / (night ? timeOfNight : timeOfDay);
        sunLight.transform.Rotate(new Vector3(rotation, 0));
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            night = !night;
            timer = night ? timeOfNight : timeOfDay;
            if (night)
            {
                onNightStart();
            } else
            {
                days += 1;
                onDayStart();
            }
        }
    }
}
