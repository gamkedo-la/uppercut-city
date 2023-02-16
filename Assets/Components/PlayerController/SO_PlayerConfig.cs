using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// make a new one when a player joins and configure it
public class SO_PlayerConfig : ScriptableObject
{
    public SO_InputData inputRedFighter;
    public SO_InputData inputBlueFighter;
    public enum Allegiance {red, neutral, blue};
    public Allegiance allegiance = Allegiance.neutral;
    public Texture2D controllerIcon;
    public void IncrementAllegiance()
    {
        int a = (int)allegiance + 1;
        Debug.Log($"Incrementing allegiance: {(Allegiance)a}");
        if(a <= 2) {allegiance = (Allegiance)a;}
    }
    public void DecrementAllegiance()
    {
        int a = (int)allegiance - 1;
        Debug.Log($"Incrementing allegiance: {(Allegiance)a}");
        if(a >= 0) {allegiance = (Allegiance)a;}
    }
    public SO_InputData GetFighterInput(){
        if(allegiance == Allegiance.red) {return inputRedFighter;}
        if(allegiance == Allegiance.blue) {return inputBlueFighter;}
        return null;
    }
}
