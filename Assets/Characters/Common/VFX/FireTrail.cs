using UnityEngine;

public class FireTrail : MonoBehaviour
{
    // Existing particles should remain after emission stops
    private ParticleSystem[] vfxParts;
    private void Awake()
    {
        vfxParts = GetComponentsInChildren<ParticleSystem>();
        Debug.Log($"vfx Components {vfxParts}");
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
        Debug.Log($"vfx Components {vfxParts}");
        foreach (ParticleSystem p in vfxParts)
        {
            p.Stop();
        }
    }
}
