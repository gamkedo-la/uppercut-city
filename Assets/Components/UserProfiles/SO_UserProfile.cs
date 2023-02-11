using System;
using UnityEngine;

[CreateAssetMenu(fileName = "UserProfile", menuName = "ScriptableObjects/UserProfile", order = 1)]
public class SO_UserProfile : ScriptableObject
{
    // keeps track of default fighter and longterm stats
    public SO_Character defaultRedFighter;
    public SO_Character defaultBlueFighter;
}
