using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIS_UserControl : AbstractAIState
{
    // for user control the AI does nothing during the match
    // At the bell it will walk the player back to the corner
    public static string stateName = "UserControl";
    public AIS_UserControl(AIController aIController) : base(aIController)
    {
    }
    public override void OnEnter()
    {

    }
    public override void OnExit()
    {

    }
    public override void OnUpdate()
    {

    }
}
