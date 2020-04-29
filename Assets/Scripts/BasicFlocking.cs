using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFlocking : MonoBehaviour
{
    public List<GameObject> boidNeighbour;
    //public SphereCollider neighbourhood = null;
    Vector3 force;
    Vector3 curVel = new Vector3();
    Vector3 v;
    Vector3 v1;
    Vector3 v2;
    Vector3 v3;
    public float separateWeight;
    public float cohesionWeight;
    public float alignmentWeight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Cohesion();
        Separation();
        Alignment();
        v += v1 + v2 + v3;
        //force = v - curVel;
        //curVel += force * Time.deltaTime;
        transform.position += (v * Time.deltaTime);
    }

    //Cohesion
    void Cohesion()
    {
        Vector3 cohesionForce = new Vector3();

        foreach (GameObject b in boidNeighbour)
        {
            cohesionForce += b.transform.position - transform.position;
        }

        if(boidNeighbour.Count != 0)
        {
            cohesionForce /= boidNeighbour.Count;
        }

        v1 = (cohesionForce - curVel) * cohesionWeight;
        //force = v1 - curVel;
        //curVel += force * Time.deltaTime;
        //transform.position += (curVel * Time.deltaTime);
    }

    //Separation Boids try to keep a small distance away from other objects (including other boids)
    void Separation()
    {
        Vector3 separateForce = new Vector3();

        foreach (GameObject b in boidNeighbour)
        {
            separateForce += (transform.position - b.transform.position);
        }

        if (boidNeighbour.Count != 0)
            separateForce /= boidNeighbour.Count;
        
        v2 = (separateForce - curVel) * separateWeight;
        //curVel += force * Time.deltaTime;
        //transform.position += (curVel * Time.deltaTime);
        //transform.rotation = Quaternion.LookRotation(separateForce);
    }

    //Alignment
    void Alignment()
    {
        Vector3 alignmentForce = new Vector3();

        foreach (GameObject b in boidNeighbour)
        {
            Agent agent = b.GetComponent<Agent>();
            alignmentForce += agent.vel;
        }

        if (boidNeighbour.Count != 0)
            alignmentForce /= boidNeighbour.Count;

        v3 = (alignmentForce - curVel) * alignmentWeight;
        //curVel += force * Time.deltaTime;
        //transform.position += (curVel * Time.deltaTime);
    }
}
