using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WristFireEmitter : MonoBehaviour
{
    public GameObject flamingWristVfx;
    private void Update() 
    {
        Instantiate(flamingWristVfx, transform.position, transform.rotation);
    }
}
