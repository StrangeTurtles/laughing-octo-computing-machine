using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidCollider : MonoBehaviour
{
    BasicFlocking basicFlocking;
    private void Awake()
    {
        basicFlocking = GetComponentInParent<BasicFlocking>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(transform.parent.gameObject == other.gameObject)
        {
            return;
        }
        if(other.tag == "Boid")
        {
            basicFlocking.boidNeighbour.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Boid")
        {
            basicFlocking.boidNeighbour.Remove(other.gameObject);
        }
    }
}
