﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPursuit : MonoBehaviour
{

    public Transform pastTarget;
    Vector3 target;

    Vector3 force;
    Vector3 v;
    Vector3 curVel;
    Vector3 vel;
    public float speed;
    public float mass;
    public float pursuitVal = 15;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // let tmpTarget be a reference to the Transform of an enemy
        target = pastTarget.position + v * pursuitVal;
        v = ((target - transform.position).normalized * speed);
        force = v - curVel;
        curVel += force * Time.deltaTime;
        transform.position += (curVel * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(v);
    }
}
