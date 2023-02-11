using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConfig : MonoBehaviour
{
    public SO_InputData fighterInput;

    public enum Allegiance {red, neutral, blue};
    public Allegiance allegiance;
    public void SetFighterInput(SO_InputData input){
        fighterInput = input;
    }
}
