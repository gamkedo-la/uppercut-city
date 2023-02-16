using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NOTE:
// this script belongs on a TRIGGER with a kinematic rigidbody and isTrigger collider
// it will detect any collider (even ones without a rb)

public class PunchDetector : MonoBehaviour
{
    AudioSource audioSource;

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
        if (other.transform.root == transform.root) {
            // Debug.Log("SELF PUNCH DETECTED: "+gameObject.name+" by "+other.gameObject.name);
            return; // ignore it
        }

        //example debug log: "Fighter (1) was BODY HIT by Fighter with RIGHT GLOVE"
        Debug.Log(transform.root.gameObject.name + " was "+gameObject.name+" by " + other.transform.root.gameObject.name + " with " +other.gameObject.name);
        
        // no effect? I can't see it - FIXME
        Debug.DrawLine(transform.position,other.transform.position,Color.red,1.5f,false);

        if (audioSource) audioSource.Play();
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

