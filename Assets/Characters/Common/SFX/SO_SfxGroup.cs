using System.Linq;
using UnityEngine;
[CreateAssetMenu(fileName = "SfxGroup", menuName = "ScriptableObjects/SfxGroup", order = 1)]
public class SO_SfxGroup : ScriptableObject
{
    public AudioClip[] sfx;
    public AudioClip GetRandomSfx()
    {
        return sfx[Random.Range(0, Mathf.Clamp(sfx.Count() - 1, 0, 99))];
    }
}
