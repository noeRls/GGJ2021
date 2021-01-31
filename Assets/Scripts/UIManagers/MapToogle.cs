using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapToogle : MonoBehaviour
{
    public CanvasRenderer canvas;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            canvas.gameObject.SetActive(!canvas.gameObject.activeSelf); 
        }
    }
}
