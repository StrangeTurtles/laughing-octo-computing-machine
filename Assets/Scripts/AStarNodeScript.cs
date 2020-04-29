using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarNodeScript : MonoBehaviour
{
    public List<GameObject> connections = new List<GameObject>();
    public GameObject prevNode = null;
    public int gScore = int.MaxValue;
    public int hScore = 0;
    public int fScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
