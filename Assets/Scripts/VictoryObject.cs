using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryObject : MonoBehaviour
{
    public List<Transform> possiblePosition;
    public GuiManager guiManager;
    private GameManager manager;
    private MeshRenderer[] meshs;
    private Color darkColor = new Color(0.1f, 0.1f, 0.1f, 0);
    private Color redColor = new Color(0.8f, 0.1f, 0.1f, 0);

    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.SetParent(possiblePosition[Random.Range(0, possiblePosition.Count - 1)], false);
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        meshs = GetComponentsInChildren<MeshRenderer>();
        onDayStart();
        manager.onDayStart += onDayStart;
        manager.onNightStart += onNightStart;
    }

    void onDayStart()
    {
        foreach (MeshRenderer mesh in meshs)
        {
            mesh.sharedMaterial.color = darkColor;
        }
    }

    void onNightStart()
    {
        foreach (MeshRenderer mesh in meshs)
        {
            mesh.sharedMaterial.color = redColor;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") &&
            Input.GetKeyDown(KeyCode.E))
        {
            guiManager.bringVictory();
        }
    }
}
