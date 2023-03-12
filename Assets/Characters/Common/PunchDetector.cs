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
    public static EventHandler OnPunchConnected;
    public Transform hitPrefab;
    public AudioSource audioSource;
    private AttackProperties attackProperties;
    public delegate void HitReceivedEvent(float damage);
    public event HitReceivedEvent onHitReceived;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        // find the parent (top most) and ignore self-punches! 
        if(other.transform.root == transform.root) {return;} // ignore it

        // handle the punch that hit us
        attackProperties = other.GetComponent<AttackProperties>();
        Debug.Log($"Hit for {attackProperties.punchDamage}");
        onHitReceived?.Invoke(attackProperties.punchDamage);
        attackProperties.gameObject.SetActive(false);
        Debug.Log(transform.root.gameObject.name + " was "+gameObject.name+" by " + other.transform.root.gameObject.name + " with " +other.gameObject.name);
        // TODO FIXME
        // since triggerEnter does not give us any contact points,
        // we can approximate the "point of contact" this way:
        // var collisionPoint = collider.ClosestPoint(transform.position);
        // var collisionNormal = transform.position - collisionPoint;

        // red line in scene view debug
        Debug.DrawLine(transform.position,other.transform.position,Color.red,1.5f,false);

        // spawn some particles
        if (hitPrefab) Instantiate(hitPrefab,other.transform.position,transform.rotation);

        // and an optional (unity native: not wwise) sound
        if (audioSource) audioSource.Play();

        OnPunchConnected?.Invoke(this, EventArgs.Empty);
    }

    /*
    // this is not used: 
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("PUNCH COLLISION DETECTED!");
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
        //if (collision.relativeVelocity.magnitude > 2)
        if (audioSource) audioSource.Play();
    }
    */

}

