using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchTrail : MonoBehaviour
{

    Vector3 targetPosition;
    Vector3 startPosition;
    float speed;
    float distance;

    private void Awake()
    {
        //Material mat = GetComponent<Renderer>().material;
        //mat.SetFloat("_RandomSeed", UnityEngine.Random.Range(0f,100f));
    }

    private void Update()
    {
        if (Vector3.Distance(startPosition, transform.position) > distance)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    public void Setup(Vector3 origin, Vector3 target, float speed, float distance)
    {
        transform.position = origin;
        startPosition = origin;
        this.speed = speed;
        this.distance = distance;
        var direction = (origin - target).normalized;
        targetPosition = target; //origin + direction * distance;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetPosition, 0.1f);
        Gizmos.DrawLine(startPosition, targetPosition);
    }
}
