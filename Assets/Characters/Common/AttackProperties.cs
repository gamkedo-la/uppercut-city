using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackProperties : MonoBehaviour
{
    public float punchDamage;
    public event PunchDetector.HitReceivedEvent onHitOpponent;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Hit Something {collision.gameObject.name}");
        if(collision.gameObject.transform.root == transform.root) {return;}
        onHitOpponent?.Invoke(punchDamage);
        gameObject.SetActive(false);
    }
}
