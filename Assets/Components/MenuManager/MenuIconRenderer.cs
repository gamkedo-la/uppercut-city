using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuIconRenderer : MonoBehaviour
{
    public SO_MenuControllerVisual chooseSidesVisual;
    private void Awake() {
        GameSystem.newPlayerJoined += InitializeMenuIcons;
        MenuManager.setupMatch += InitializeMenuIcons;
    }
    private void MapInputsToIcons()
    {
        chooseSidesVisual.SetupInputIcons(FindObjectsOfType<PlayerController>());
    }
    private void InitializeMenuIcons(object sender, System.EventArgs e)
    {
        MapInputsToIcons();
        // iterate through Inputs Icons
        // set the correct menu icon and color
        // set the icon to 'active'
    }
    private void FixedUpdate() 
    {
        // trigger the icons to move towards the correct menu spot
        Debug.Log("MenuIconRenderer FU");
    }
}
