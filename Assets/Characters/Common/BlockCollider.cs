using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollider : MonoBehaviour
{
    public delegate void BlockedPunch();
    public SO_SfxGroup sfxBlockLight;
    public SO_VfxGroup vfxBlockLight;
    private AudioSource audioSource;
    private ContactPoint collisionPoint;
    public event BlockedPunch onBlockedPunch;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.transform.root == transform.root) {return;}
        Debug.Log($"Punch Blocked {collision.gameObject.name}");

        collisionPoint = collision.GetContact(0);
        if (vfxBlockLight) Instantiate(vfxBlockLight,collisionPoint.point, Quaternion.LookRotation(collisionPoint.normal));
        if (audioSource && sfxBlockLight) audioSource.PlayOneShot(sfxBlockLight.GetRandomSfx());
        onBlockedPunch?.Invoke();
        // trigger opponent's 'PunchBlocked' handler
        // play the blocked VFX
    }
}
