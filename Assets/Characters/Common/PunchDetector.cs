using System;
using System.Collections.Generic;
using UnityEngine;

// NOTE:
// this script belongs on a TRIGGER with a kinematic rigidbody and isTrigger collider
// it will detect any collider (even ones without a rb)

public class PunchDetector : MonoBehaviour
{
    public SO_FighterConfig fighterConfig;
    public Animator fighterAnimator;
    public Transform hitPrefab;
    public AudioSource audioSource;
    private AttackProperties attackProperties;
    private ContactPoint collisionPoint;
    public delegate void HitReceivedEvent(float damage);
    public event HitReceivedEvent onHitReceived;
    public static HitReceivedEvent onPunchConnected;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        // find the parent (top most) and ignore self-punches! 
        if(collision.gameObject.transform.root == transform.root) {return;}
        // get the attack properties of the opponent's glove
        attackProperties = collision.gameObject.GetComponent<AttackProperties>();
        onHitReceived?.Invoke(attackProperties.punchDamage);
        onPunchConnected?.Invoke(attackProperties.punchDamage);
        Debug.Log($"{transform.root.gameObject.name} got {gameObject.name} for {attackProperties.punchDamage}");

        collisionPoint = collision.GetContact(0);

        // spawn some particles
        if (hitPrefab) Instantiate(hitPrefab,collisionPoint.point, Quaternion.LookRotation(collisionPoint.normal));

        // and an optional (unity native: not wise) sound
        if (audioSource) audioSource.Play();
    }

}

