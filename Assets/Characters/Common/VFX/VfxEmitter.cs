using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxEmitter : MonoBehaviour
{
    public SO_VfxGroup vfxGroup;
    private void Update() 
    {
        Instantiate(vfxGroup.GetRandomVfx(), transform.position, transform.rotation);
    }
}
