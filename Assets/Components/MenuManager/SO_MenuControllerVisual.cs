using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "MenuControllerVisualizer", menuName = "ScriptableObjects/MenuControllerVisualizer", order = 1)]
public class SO_MenuControllerVisual : ScriptableObject
{
    // multiple controller icons for the 4 possible players
    public Sprite[] controllerIcons = new Sprite[4];
    private GameObject[] inputIcon;
    private PlayerInput[] playerInputs;
    public void SetActiveInputIcons(PlayerInput[] inputs)
    {
        playerInputs = inputs;
        for (int i = 0; i < playerInputs.Length; i++)
        {
            // map the active players to input icons
        }
    }
}
