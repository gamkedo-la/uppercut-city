using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackProperties : MonoBehaviour
{
    public float punchDamage;
    public event PunchDetector.HitReceivedEvent onHitOpponent;
    public event PunchDetector.HitReceivedEvent onGotBlocked;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.transform.root == transform.root) {return;}

        // block or hit?
        Debug.Log($"Hit Something, layer {collision.gameObject.layer}");
        switch (collision.gameObject.layer)
        {
            case 7: // hit
                onHitOpponent?.Invoke(punchDamage);
                break;
            case 8: // block
                onGotBlocked?.Invoke(punchDamage);
                punchDamage = 0;
                break;
            default:
            gameObject.SetActive(false);
                break;
        }
    }
}
