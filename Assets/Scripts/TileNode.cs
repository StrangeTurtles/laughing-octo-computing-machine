using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TileNode : MonoBehaviour
{
    public int dimensions = 0;
    public GameObject node;
    public List<GameObject> nodeList = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < dimensions; i++)
        {
            for (int j = 0; j < dimensions; j++)
            {
                nodeList.Add(Instantiate(node, new Vector3(i * 2.0f, 0, j * 2.0f), Quaternion.identity));
            }
        }
        for (int i = 0; i < nodeList.Count; i++)
        {
            if (i - dimensions > -1)
            {
                nodeList[i].GetComponent<NodeScript>().connections.Add(nodeList[i - dimensions]);
            }
            if (i + dimensions <= nodeList.Count - 1)
            {
                nodeList[i].GetComponent<NodeScript>().connections.Add(nodeList[i + dimensions]);
            }
            if (i - 1 > -1)
            {
                nodeList[i].GetComponent<NodeScript>().connections.Add(nodeList[i - 1]);
            }
            if (i + 1 <= nodeList.Count - 1)
            {
                nodeList[i].GetComponent<NodeScript>().connections.Add(nodeList[i + 1]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
