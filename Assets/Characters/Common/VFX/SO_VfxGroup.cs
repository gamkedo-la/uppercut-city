using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[CreateAssetMenu(fileName = "VfxGroup", menuName = "ScriptableObjects/VfxGroup", order = 1)]
public class SO_VfxGroup : ScriptableObject
{
    public GameObject[] vfx;
    public GameObject GetRandomVfx()
    {
        int max = Mathf.Clamp(vfx.Count() - 1, 0, 99) ;
        return vfx[Random.Range(0, max)];
    }
}
