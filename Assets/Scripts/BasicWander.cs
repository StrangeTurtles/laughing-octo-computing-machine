using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWander : MonoBehaviour
{
    public Vector3 target;
    //public GameObject gameObject;

    Vector3 force;
    Vector3 v;
    public Vector3 curVel;
    Vector3 vel;
    public float speed;
    public float mass;
    public float maxTime;
    public float now;
    // Start is called before the first frame update
    void Start()
    {
        //vel = GetComponent<Rigidbody>();
        //curVel = vel.velocity;
        now = maxTime;
    }

    //Calculate the wander force
    private Vector3 Wander()
    {
        target = Random.insideUnitSphere;
        return target;
    }

    // Update is called once per frame
    void Update()
    {
        
        //now = Random.Range((float)0f, (float)15f);
        if(now >= maxTime)
        {
            Wander();
            target *= speed;
            target = new Vector3(target.x, .25f, target.z);
            now = 0;
        }
        now++;
        //v = Wander();
        //v = Vector3.ClampMagnitude(v, speed); 
        //v /= mass;
        v = ((target - transform.position) * speed).normalized;
        force = v - curVel;
        curVel += force * Time.deltaTime;
        
        transform.position += curVel * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(v);
    }
}
