using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapPointer : MonoBehaviour
{
    private GameObject player;
    private RectTransform pos;
    private Vector3 mapCenter = new Vector3(500, 0, 500);

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pos = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 relativePos = (player.transform.position - mapCenter) / 10;
        Vector3 newPos = pos.anchoredPosition;
        newPos.x = relativePos.x;
        newPos.y = relativePos.z;
        pos.anchoredPosition = newPos;
    }
}
