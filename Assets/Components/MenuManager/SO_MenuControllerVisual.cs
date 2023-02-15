using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "MenuControllerVisualizer", menuName = "ScriptableObjects/MenuControllerVisualizer", order = 1)]
public class SO_MenuControllerVisual : ScriptableObject
{
    // multiple controller icons for the 4 possible players
    private List<GameObject> inputIcons;
    private PlayerController[] playerControllers;
    public void SetupInputIcons(PlayerController[] inputs)
    {
        playerControllers = inputs;
        inputIcons = new List<GameObject>();
        foreach (PlayerController controller in playerControllers)
        {
            // add the icon to the gameobject list
            inputIcons.Add(controller.playerConfig.controllerIcon);
        }
        for (int i = 0; i < playerControllers.Length; i++)
        {
            // map the active players to input icons
            switch (playerControllers[i].playerConfig.allegiance)
            {
                case SO_PlayerConfig.Allegiance.red:
                    // set the icon to red
                    break;
                case SO_PlayerConfig.Allegiance.neutral:
                    // set the icon to red
                    break;
                case SO_PlayerConfig.Allegiance.blue:
                    // set the icon to red
                    break;
                default:
                    Debug.Log("No allegiance? - Debug");
                    break;
            }
        }
    }
}
