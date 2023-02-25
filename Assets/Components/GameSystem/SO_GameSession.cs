using System;
using UnityEngine.InputSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSession", menuName = "ScriptableObjects/GameSession", order = 1)]
public class SO_GameSession : ScriptableObject
{
    public PlayerInput[] localInputs;
}
