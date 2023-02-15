using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// make a new one when a player joins and configure it
public class SO_PlayerConfig : ScriptableObject
{
    public SO_InputData fighterInput;

    public enum Allegiance {red, neutral, blue};
    public Allegiance allegiance = Allegiance.neutral;
    public Texture2D controllerIcon;
}
