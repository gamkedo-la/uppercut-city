using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuIconRenderer : MonoBehaviour
{
    public SO_MenuControllerVisual chooseSidesVisual;
    // get active inputs and set them in th SO_MenuControllerVisual
    private void MapInputsToIcons()
    {
        chooseSidesVisual.SetActiveInputIcons(FindObjectsOfType<PlayerController>());
    }
    private void FixedUpdate() 
    {
        // Get all controllers
        // trigger the icons to move towards the correct menu spot
        Debug.Log("MenuIconRenderer FU");
    }
}
