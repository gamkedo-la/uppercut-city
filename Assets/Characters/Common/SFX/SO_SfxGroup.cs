using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[CreateAssetMenu(fileName = "SfxGroup", menuName = "ScriptableObjects/SfxGroup", order = 1)]
public class SO_SfxGroup : ScriptableObject
{
    public AudioClip[] sfx;
    public AudioClip GetRandomSfx()
    {
        int max = Mathf.Clamp(sfx.Count() - 1, 0, 99);
        return sfx[Random.Range(0, max)];
    }
}
