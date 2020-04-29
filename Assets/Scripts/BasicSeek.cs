using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSeek : MonoBehaviour
{
    public GameObject target;

    Vector3 force;
    Vector3 v;
    Vector3 curVel;
    Vector3 vel;
    public float speed;
    public float mass;
    // Start is called before the first frame update
    void Start()
    {
        //vel = GetComponent<Rigidbody>();
        //curVel = vel.velocity;
    }

    // Update is called once per frame
    void Update()
    {
        // let tmpTarget be a reference to the Transform of an enemy
        v = ((target.transform.position - transform.position).normalized * speed);
        force = v - curVel;
        curVel += force * Time.deltaTime;
        transform.position += curVel * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(v);
    }
}
