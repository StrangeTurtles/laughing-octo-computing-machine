using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Djikstra : MonoBehaviour
{
    public GameObject startNode = null;
    public GameObject endNode = null;
    List<GameObject> unvisited = new List<GameObject>();
    List<GameObject> visited = new List<GameObject>();
    List<GameObject> path = new List<GameObject>();
    GameObject curNode = null;
    //GameObject prevNode = null;
    public bool started = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(startNode != null && endNode != null && started)
        {
            if(started)
            {
                unvisited.Add(startNode);
                startNode.GetComponent<NodeScript>().gScore = 0;
                curNode = startNode;
                started = !started;
            }
            while(curNode != endNode || unvisited.Count != 0)
            {
                //curNode.GetComponent<NodeScript>().prevNode = prevNode;
                foreach(var b in curNode.GetComponent<NodeScript>().connections)
                {
                    if(b.GetComponent<NodeScript>().gScore > curNode.GetComponent<NodeScript>().gScore)
                    {
                        b.GetComponent<NodeScript>().gScore = curNode.GetComponent<NodeScript>().gScore + 1;
                        b.GetComponent<NodeScript>().prevNode = curNode;
                        unvisited.Add(b);
                    }
                }
                unvisited.Remove(curNode);
                //prevNode = curNode;
                if(curNode == endNode)
                {
                    break;
                }
                curNode = unvisited[0];
            }
            curNode = endNode;
            while(curNode.GetComponent<NodeScript>().prevNode != null)
            {
                path.Add(curNode);
                curNode = curNode.GetComponent<NodeScript>().prevNode;
            }

            for (int i = path.Count - 1; i >= 0; i--)
            {
                transform.position = path[i].transform.position;
            }
        }
    }
}
