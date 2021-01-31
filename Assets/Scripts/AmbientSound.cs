using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSound : MonoBehaviour
{

    public AudioClip DayAmbient;
    public AudioClip NightAmbient;
    public AudioClip ShopAmbient;
    private AudioSource AmbientSource;

    // Start is called before the first frame update
    void Start()
    {
        AmbientSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

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
}