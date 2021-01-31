using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCoin : MonoBehaviour
{

    public AudioClip CoinWaiting;
    public AudioClip CoinCollecting;
    private AudioSource CoinAudio;
    private Collider CoinCollider;
    PlayerStats stats;

    // Start is called before the first frame update
    void Start()
    {
        CoinCollider = GetComponent<Collider>();
        CoinAudio = GetComponent<AudioSource>();
        CoinAudio.clip = CoinWaiting;
        CoinAudio.Play();
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 50 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine("DestroyCoin");
    }

    IEnumerator DestroyCoin()
    {
        CoinAudio.Stop();
        CoinAudio.clip = CoinCollecting;
        CoinAudio.Play();
        yield return new WaitForSeconds(0.5f);
        CoinAudio.Stop();
        Destroy(gameObject);
        stats.money += 10;
    }
}
