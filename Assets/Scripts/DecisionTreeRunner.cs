using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PrintDecision;

public class DecisionTreeRunner : MonoBehaviour
{
    public IDecision decisionTreeRoot;
    // Start is called before the first frame update
    void Start()
    {
        decisionTreeRoot = new PrintDecision(true);

        decisionTreeRoot.MakeDecision();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
