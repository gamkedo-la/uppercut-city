using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "MenuControllerVisualizer", menuName = "ScriptableObjects/MenuControllerVisualizer", order = 1)]
public class SO_MenuControllerVisual : ScriptableObject
{
    // multiple controller icons for the 4 possible players
    private GameObject[] inputIcon;
    private PlayerController[] playerControllers;
    public void SetActiveInputIcons(PlayerController[] inputs)
    {
        playerControllers = inputs;
        for (int i = 0; i < playerControllers.Length; i++)
        {
            // map the active players to input icons
        }
    }
}
