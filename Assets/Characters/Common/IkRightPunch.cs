using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class IkRightPunch : MonoBehaviour
{
    // handle behaviors related to the right punch
    private Rig rightArmIk;
    private void Awake()
    {
        rightArmIk = GetComponent<Rig>();
        // subscribe to events
    }
    public void SetIkWeight(float weight)
    {
        rightArmIk.weight = weight;
        Debug.Log("ik weight to " + rightArmIk.weight);

    }
}
