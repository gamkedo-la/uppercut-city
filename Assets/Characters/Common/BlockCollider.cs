using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollider : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log($"Punch Blocked");
        // trigger opponent's 'PunchBlocked' handler
        // play the blocked VFX
    }
}
