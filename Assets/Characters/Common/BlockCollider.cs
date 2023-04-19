using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollider : MonoBehaviour
{
    public delegate void BlockedPunch(AudioClip blockSound);
    public SO_SfxGroup sfxBlockLight;
    public SO_VfxGroup vfxBlockLight;
    private AudioSource audioSource;
    private ContactPoint collisionPoint;
    public event BlockedPunch onBlockedPunch;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();    
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.transform.root == transform.root) {return;}

        collisionPoint = collision.GetContact(0);
        if (vfxBlockLight) Instantiate(vfxBlockLight,collisionPoint.point, Quaternion.LookRotation(collisionPoint.normal));
        onBlockedPunch?.Invoke(sfxBlockLight.GetRandomSfx());
        // trigger opponent's 'PunchBlocked' handler
        // play the blocked VFX
    }
}
