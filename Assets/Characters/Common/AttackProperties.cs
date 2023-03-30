using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackProperties : MonoBehaviour
{
    public float punchDamage;
    public event PunchDetector.HitReceivedEvent onHitOpponent;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"attack properties trigger");
        if(other.transform.root == transform.root) {return;}
        onHitOpponent?.Invoke(punchDamage);
        // disable punches
    }
}
