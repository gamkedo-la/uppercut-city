using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameType", menuName = "ScriptableObjects/GameType", order = 1)]
public class SO_GameType : ScriptableObject
{
    // keeps track of data related to game rules
    // other objects will reference this object for game data
    public int numberOfRounds = 8;
    public int currentRound = 1;
    public float roundTime = 120f;
    public float restTime = 30f;
    public void ResetMatch(){
        currentRound = 1;
    }
}
