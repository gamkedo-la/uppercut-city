using System;
using UnityEngine;

public class FighterConfig : MonoBehaviour
{
    // Establish What Corner the Fighter is in
    // Establish what character is being used
    private static readonly string[] cornerOptions = {"red", "blue"};
    public enum Corner {red, blue};
    public Corner corner;
    public SO_Character[] playableCharacters;

}
