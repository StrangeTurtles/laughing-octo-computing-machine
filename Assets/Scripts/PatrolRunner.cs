using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolRunner : MonoBehaviour
{
    public Transform[] waypoints = null;
    public int curTarget = 0;
    public IDecision PatrolRoot;
    // Start is called before the first frame update
    void Start()
    {
        PatrolRoot = new WaypointReached(this,
            new GetNewWaypoint(this),
            new MoveToWaypoint(this));
    }

    // Update is called once per frame
    void Update()
    {
        IDecision curDecision = PatrolRoot.MakeDecision();

        while(curDecision.MakeDecision() != null)
        {
            PatrolRoot.MakeDecision();
            curDecision = PatrolRoot.MakeDecision();
        }
    }
}

public class WaypointReached : IDecision
{
    PatrolRunner guard;
    IDecision left;
    IDecision right;
    public WaypointReached(){}

    public WaypointReached(PatrolRunner guard, IDecision left, IDecision right)
    {
        this.guard = guard;
        this.left = left;
        this.right = right;
    }

    public IDecision MakeDecision()
    {
        float distance = Mathf.Sqrt(Mathf.Pow(guard.transform.position.x - guard.waypoints[guard.curTarget].position.x, 2) + Mathf.Pow(guard.transform.position.z - guard.waypoints[guard.curTarget].position.z, 2));

        return ((distance >= -.5 && distance <= .5) ? left : right);
    }
}

public class GetNewWaypoint : IDecision
{
    PatrolRunner guard;

    public GetNewWaypoint() { }

    public GetNewWaypoint(PatrolRunner guard)
    {
        this.guard = guard;
    }

    public IDecision MakeDecision()
    {
        //Debug.Log("new waypoint");
        if(guard.curTarget >= guard.waypoints.Length - 1)
        {
            guard.curTarget = 0;
        }
        else
        {
            guard.curTarget++;
        }
        return null;
    }
}

public class MoveToWaypoint : IDecision
{
    PatrolRunner guard;

    public MoveToWaypoint() { }

    public MoveToWaypoint(PatrolRunner guard)
    {
        this.guard = guard;
    }
    public IDecision MakeDecision()
    {
        guard.transform.position += ((guard.waypoints[guard.curTarget].transform.position - guard.transform.position).normalized * .04f);
        return null;
    }
}