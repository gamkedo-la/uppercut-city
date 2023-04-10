using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine;

// make a new one when a player joins and configure it
public class SO_PlayerConfig : ScriptableObject
{
    public enum Allegiance {red, neutral, blue};
    public Allegiance allegiance = Allegiance.neutral;
    public Texture2D controllerIcon;
    public PlayerInput playerInput;
    public PlayerInputHandling playerInputHandling;
    public UIInputHandling UiInputHandling;
    public void IncrementAllegiance()
    {
        int a = (int)allegiance + 1;
        if(a <= 2) {allegiance = (Allegiance)a;}
    }
    public void DecrementAllegiance()
    {
        int a = (int)allegiance - 1;
        if(a >= 0) {allegiance = (Allegiance)a;}
    }
}
