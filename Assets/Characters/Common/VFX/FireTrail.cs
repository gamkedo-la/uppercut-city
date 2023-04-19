using UnityEngine;

public class FireTrail : MonoBehaviour
{
    // Existing particles should remain after emission stops
    private ParticleSystem[] vfxParts;
    private void Awake()
    {
        vfxParts = GetComponentsInChildren<ParticleSystem>();
    }
    public void EmitParticles()
    {
        foreach (ParticleSystem p in vfxParts)
        {
            p.Clear();
            p.Play();
        }
    }
    public void StopEmission()
    {
        foreach (ParticleSystem p in vfxParts)
        {
            p.Stop();
        }
    }
}
