using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPreview : MonoBehaviour
{
    public BoxCollider disableCollider;
    public Rigidbody disableRigidbody;
    public List<MonoBehaviour> scriptToDisable;
    // Start is called before the first frame update
    private void Awake()
    {
        if (disableCollider != null) disableCollider.enabled = false;
        if (disableRigidbody != null) disableRigidbody.isKinematic = true;
        foreach (MonoBehaviour c in scriptToDisable)
        {
            c.enabled = false;
        }
    }

    public void cancel()
    {
        Destroy(gameObject);
    }

    public void confirm()
    {
        transform.parent = null;
        if (disableCollider != null) disableCollider.enabled = true;
        if (disableRigidbody != null) disableRigidbody.isKinematic = false;
        foreach (MonoBehaviour disabledComponent in scriptToDisable)
        {
            disabledComponent.enabled = true;
        }
    }
}
