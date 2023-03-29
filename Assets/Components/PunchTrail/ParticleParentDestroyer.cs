using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleParentDestroyer : MonoBehaviour
{
    public void OnParticleSystemStopped() {
        Destroy(transform.parent.gameObject);
    }
}
