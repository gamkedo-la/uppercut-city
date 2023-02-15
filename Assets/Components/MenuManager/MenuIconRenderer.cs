using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuIconRenderer : MonoBehaviour
{
    [SerializeField] List<GameObject> inputMenuSprites;
    private PlayerController[] inputs;
    private void Awake()
    {
        GameSystem.newPlayerJoined += InitializeMenuIcons;
        MenuManager.setupMatch += InitializeMenuIcons;
    }
    private void HideAllMenuSprites()
    {
        foreach (GameObject icon in inputMenuSprites)
        {
            icon.SetActive(false);
        }
    }
    private void MapInputsToIcons()
    {
        // chooseSidesVisual.SetupInputIcons(FindObjectsOfType<PlayerController>());
    }
    private void InitializeMenuIcons(object sender, System.EventArgs e)
    {
        Debug.Log("Initializing menu icons");
        HideAllMenuSprites();
        inputs = FindObjectsOfType<PlayerController>();
        for (int i=0; i < inputs.Length; i++)
        {
            inputMenuSprites[i].gameObject.SetActive(true);
            // inputMenuSprites[i].GetComponentsInChildren<RawImage>()[1].texture = inputs[i].playerConfig.controllerIcon;
            // set the icon type
        }
    }
    private void Update() 
    {
        // move the icons from current transform towards intended
    }
}
