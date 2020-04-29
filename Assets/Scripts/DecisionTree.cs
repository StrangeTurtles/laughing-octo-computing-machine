using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintDecision : IDecision
{
    public bool branch = false;


    public PrintDecision() { }

    public PrintDecision(bool branch)
    {
        this.branch = branch;
    }

    public IDecision MakeDecision()
    {
        Debug.Log(branch ? "Yes" : "No");

        return null;
    }
}
