using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterGroup", menuName = "ScriptableObjects/CharacterGroup", order = 1)]
public class SO_CharacterGroup : ScriptableObject
{
    public SO_Character[] characters;
}
