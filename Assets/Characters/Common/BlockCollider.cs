using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollider : MonoBehaviour
{
    public delegate void BlockedPunch();
    public event BlockedPunch onBlockedPunch;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Punch Blocked {collision.gameObject.name}");
        onBlockedPunch?.Invoke();
        // trigger opponent's 'PunchBlocked' handler
        // play the blocked VFX
    }
}
