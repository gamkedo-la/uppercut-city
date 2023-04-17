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
        Debug.Log($"{gameObject.name} Hit Something {collision.gameObject.name}");
        if(collision.gameObject.transform.root == transform.root) {return;}

        // block or hit?
        
        switch (collision.gameObject.layer)
        {
            case 7: // hit
                onHitOpponent?.Invoke(punchDamage);
                break;
            case 8: // block
                onGotBlocked?.Invoke(punchDamage);
                punchDamage = 0;
                break;
            case 10:
                // ignore model layer collisions
                break;
            default:
                gameObject.SetActive(false);
                break;
        }
    }
}
