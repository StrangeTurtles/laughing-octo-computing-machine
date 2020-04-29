using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeConnect : MonoBehaviour
{
    NodeScript nodeScript;

    private void Awake()
    {
        nodeScript = GetComponentInParent<NodeScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (transform.parent.gameObject == other.gameObject)
        {
            return;
        }
        if (other.tag == "Node")
        {
            nodeScript.connections.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Node")
        {
            nodeScript.connections.Remove(other.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
