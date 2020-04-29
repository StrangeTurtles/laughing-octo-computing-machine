using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    public GameObject startNode = null;
    public GameObject endNode = null;
    List<GameObject> unvisited = new List<GameObject>();
    List<GameObject> visited = new List<GameObject>();
    List<GameObject> path = new List<GameObject>();
    GameObject curNode = null;
    bool started = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startNode != null && endNode != null && started)
        {
            if (started)
            {
                unvisited.Add(startNode);
                startNode.GetComponent<AStarNodeScript>().gScore = 0;
                curNode = startNode;
                started = !started;
            }
            while (curNode != endNode || unvisited.Count != 0)
            {
                //curNode.GetComponent<NodeScript>().prevNode = prevNode;
                foreach (var b in curNode.GetComponent<AStarNodeScript>().connections)
                {
                    int temp = 0;
                    temp += Mathf.Abs( (int) b.transform.position.x - (int) endNode.transform.position.x);
                    temp += Mathf.Abs((int)b.transform.position.z - (int)endNode.transform.position.z);
                    b.GetComponent<AStarNodeScript>().hScore = temp;
                    
                    if (b.GetComponent<AStarNodeScript>().gScore > curNode.GetComponent<AStarNodeScript>().gScore)
                    {
                        b.GetComponent<AStarNodeScript>().gScore = curNode.GetComponent<AStarNodeScript>().gScore + 1;
                        b.GetComponent<AStarNodeScript>().fScore = b.GetComponent<AStarNodeScript>().hScore + b.GetComponent<AStarNodeScript>().gScore;
                        b.GetComponent<AStarNodeScript>().prevNode = curNode;
                        unvisited.Add(b);
                    }
                }
                unvisited.Remove(curNode);
                //prevNode = curNode;
                if (curNode == endNode)
                {
                    break;
                }

                unvisited.Sort((v1,v2)=>v1.GetComponent<AStarNodeScript>().fScore.CompareTo(v2.GetComponent<AStarNodeScript>().fScore));

                curNode = unvisited[0];
            }
            curNode = endNode;
            while (curNode.GetComponent<AStarNodeScript>().prevNode != null)
            {
                path.Add(curNode);
                curNode = curNode.GetComponent<AStarNodeScript>().prevNode;
            }

            for (int i = path.Count - 1; i >= 0; i--)
            {
                transform.position = path[i].transform.position;
            }
        }
    }

    void SortByFScore(List<GameObject> sort)
    {

    }
}
