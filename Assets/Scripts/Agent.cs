using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    Vector3 prevVel;
    Vector3 curVel;
    Vector3 v;
    public Vector3 vel;
    Vector3 force;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        curVel = transform.position;
        prevVel = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        curVel = transform.position;
        vel = ((transform.position - prevVel).normalized * speed);
        //v = ((target - transform.position).normalized * speed);
        force = vel - curVel;
        curVel += force * Time.deltaTime;
        //transform.position += (curVel * Time.deltaTime);
    }

    private void LateUpdate()
    {
        prevVel = transform.position;
    }
}
