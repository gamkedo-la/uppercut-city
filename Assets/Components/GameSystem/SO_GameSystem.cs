using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSystem", menuName = "ScriptableObjects/GameSystem", order = 1)]
public class SO_GameSystem : ScriptableObject
{
    // keeps track of data related to game flow
    // other objects will reference this object for game data
    public int numberOfRounds = 8;
    public int currentRound = 1;
    public float roundTime = 60f;
    public float restTime = 30f;
    public void ResetMatch(){
        currentRound = 1;
    }
}
