using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSound : MonoBehaviour
{

    public AudioClip DayAmbient;
    public AudioClip NightAmbient;
    public AudioClip ShopAmbient;
    private AudioSource AmbientSource;
    private GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        manager.onDayStart += PlayDayAmbient;
        manager.onNightStart += PlayNightAmbient;
        AmbientSource = GetComponent<AudioSource>();
        PlayDayAmbient();
    }

    //When sunrise, Day ambient sound start and night's one stop
    public void PlayDayAmbient()
    {
        AmbientSource.Stop();
        AmbientSource.clip = DayAmbient;
        AmbientSource.Play();
    }

    //When sunset, Night ambient sound start and day's one stop
    public void PlayNightAmbient()
    {
        AmbientSource.Stop();
        AmbientSource.clip = NightAmbient;
        AmbientSource.Play();
    }

    public void PlayShopAmbient()
    {
        AmbientSource.Stop();
        AmbientSource.clip = ShopAmbient;
        AmbientSource.Play();
    }

    public void StopPlayingShopAmbiant()
    {
        if (manager.night)
        {
            PlayNightAmbient();
        } else {
            PlayDayAmbient();
        }
    }
}