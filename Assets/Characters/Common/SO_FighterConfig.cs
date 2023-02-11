using System;
using UnityEngine;

[CreateAssetMenu(fileName = "FighterConfig", menuName = "ScriptableObjects/FighterConfig", order = 1)]
public class SO_FighterConfig : ScriptableObject
{
    // Establish What Corner the Fighter is in
    // Establish what character is being used
    public int characterIndex;
}
