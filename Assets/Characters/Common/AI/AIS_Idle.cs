using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIS_Idle : AbstractAIState
{
    public static string stateName = "Idle";
    public AIController aIcontroller;
    public AIS_Idle(AIController aic) : base(aic)
    {
        aIcontroller = aic;
    }
    public override void OnEnter()
    {
        // what whould we do when we enter Idle?

        Debug.Log($"{aIcontroller.name} Enter:  Idle");
    }
    public override void OnUpdate()
    {
        // what whould we do while we're Idle?
    }

    public override void OnExit()
    {
        // what whould we do when we exit Idle?
    }
}
