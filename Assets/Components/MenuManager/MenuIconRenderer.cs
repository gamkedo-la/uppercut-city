using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuIconRenderer : MonoBehaviour
{
    [SerializeField] [Range(600f, 1800f)] float iconSpeed;
    [SerializeField] List<GameObject> inputMenuSprites;
    [SerializeField] TimeProvider menuTimeProvider;
    [SerializeField] List<GameObject> neutralSlots;
    [SerializeField] GameObject redCornerSlot;
    [SerializeField] GameObject blueCornerSlot;
    private PlayerController[] inputs;
    private int neutralPlayerCount = 0;
    private void Awake()
    {
        PlayerController.newPlayerJoined += InitializeMenuIcons;
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
            inputMenuSprites[i].GetComponentsInChildren<RawImage>()[1].texture = inputs[i].playerConfig.controllerIcon;
            // set the icon type
        }
    }
    private void MoveMenuIcons()
    {
        // move towards the object associated with the playerconfig allegiance
        neutralPlayerCount = 0;
        for (int i=0; i < inputs.Length; i++)
        {
            switch (inputs[i].playerConfig.allegiance)
            {
                case SO_PlayerConfig.Allegiance.red:
                    inputMenuSprites[i].transform.position = Vector3.MoveTowards(
                        inputMenuSprites[i].transform.position, // current
                        redCornerSlot.transform.position, // target
                        iconSpeed*menuTimeProvider.deltaTime // max displacement
                    );
                    break;
                case SO_PlayerConfig.Allegiance.blue:
                    inputMenuSprites[i].transform.position = Vector3.MoveTowards(
                        inputMenuSprites[i].transform.position, // current
                        blueCornerSlot.transform.position, // target
                        iconSpeed*menuTimeProvider.deltaTime // max displacement
                    );
                    break;
                case SO_PlayerConfig.Allegiance.neutral:
                    inputMenuSprites[i].transform.position = Vector3.MoveTowards(
                        inputMenuSprites[i].transform.position, // current
                        neutralSlots[neutralPlayerCount].transform.position, // target
                        iconSpeed*menuTimeProvider.deltaTime // max displacement
                    );
                    break;
            }
        }
    }
    private void Update() 
    {
        // move the icons from current transform towards intended
        MoveMenuIcons();
    }
}
