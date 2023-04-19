using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuIconRenderer : MonoBehaviour
{
    [SerializeField] [Range(1800f, 5000f)] float iconSpeed;
    [SerializeField] List<GameObject> inputMenuSprites;
    [SerializeField] TimeProvider menuTimeProvider;
    [SerializeField] List<GameObject> neutralSlots;
    [SerializeField] GameObject redCornerSlot;
    [SerializeField] GameObject blueCornerSlot;
    private PlayerController[] inputs;
    private int neutralPlayerCount = 0;
    private bool redHasController, blueHasController;
    private bool sideSelectionCooldown = false;
    private void Awake()
    {
        PlayerController.newPlayerJoined += InitializeMenuIcons;
    }
    private void OnEnable()
    {
        Debug.Log("MenuIcons renderer enabled");
        InitializeMenuIcons();
        UIInputHandling.onSideSelection += HandleSideSelection; 
    }
    private void OnDisable()
    {
        Debug.Log("MenuIcons renderer disabled");
        UIInputHandling.onSideSelection -= HandleSideSelection; 
    }
    private void HideAllMenuSprites()
    {
        foreach (GameObject icon in inputMenuSprites)
        {
            icon.SetActive(false);
        }
    }
    private IEnumerator ChooseSidesCooldown()
    {
        yield return new WaitForSeconds(menuTimeProvider.fixedDeltaTime * 5);
        sideSelectionCooldown = false;
    }
    private void HandleSideSelection(SO_PlayerConfig config, float sideSelectionAxis)
    {
        if(sideSelectionAxis != 0 && !sideSelectionCooldown){
            sideSelectionCooldown = true;
            StartCoroutine(ChooseSidesCooldown());
            // can we join next party?
            // where are we now?
            if(sideSelectionAxis > 0){ // trying to move right
                switch (config.allegiance)
                {
                    case SO_PlayerConfig.Allegiance.red:
                        redHasController = false;
                        config.allegiance = SO_PlayerConfig.Allegiance.neutral;
                        break;
                    case SO_PlayerConfig.Allegiance.neutral:
                        if(!blueHasController)
                        {
                            blueHasController = true;
                            config.allegiance = SO_PlayerConfig.Allegiance.blue;
                        }
                        break;
                    default:
                        break;
                }
            } else if(sideSelectionAxis < 0) { // trying to move left
                switch (config.allegiance)
                {
                    case SO_PlayerConfig.Allegiance.neutral:
                        if(!redHasController)
                        {
                            redHasController = true;
                            config.allegiance = SO_PlayerConfig.Allegiance.red;
                        }
                        break;
                    case SO_PlayerConfig.Allegiance.blue:
                        blueHasController = false;
                        config.allegiance = SO_PlayerConfig.Allegiance.neutral;
                        break;
                    default:
                        break;
                }
            }
        }
    }
    private void InitializeMenuIcons()
    {
        Debug.Log("Initializing menu icons");
        HideAllMenuSprites();
        redHasController = false;
        blueHasController = false;
        inputs = FindObjectsOfType<PlayerController>();
        for (int i=0; i < inputs.Length; i++)
        {
            // determine if either corner has a controller
            if(inputs[i].playerConfig.allegiance == SO_PlayerConfig.Allegiance.red) {redHasController = true;}
            if(inputs[i].playerConfig.allegiance == SO_PlayerConfig.Allegiance.blue) {blueHasController = true;}
                
            inputMenuSprites[(inputs.Length-1) - i].gameObject.SetActive(true);
            inputMenuSprites[(inputs.Length-1) - i].GetComponentsInChildren<RawImage>()[1].texture = inputs[i].playerConfig.controllerIcon;
            // (inputs.Length-1) - i
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
                    inputMenuSprites[(inputs.Length-1) - i].transform.position = Vector3.MoveTowards(
                        inputMenuSprites[(inputs.Length-1) - i].transform.position, // current
                        redCornerSlot.transform.position, // target
                        iconSpeed*menuTimeProvider.deltaTime // max displacement
                    );
                    break;
                case SO_PlayerConfig.Allegiance.blue:
                    inputMenuSprites[(inputs.Length-1) - i].transform.position = Vector3.MoveTowards(
                        inputMenuSprites[(inputs.Length-1) - i].transform.position, // current
                        blueCornerSlot.transform.position, // target
                        iconSpeed*menuTimeProvider.deltaTime // max displacement
                    );
                    break;
                case SO_PlayerConfig.Allegiance.neutral:
                    inputMenuSprites[(inputs.Length-1) - i].transform.position = Vector3.MoveTowards(
                        inputMenuSprites[(inputs.Length-1) - i].transform.position, // current
                        neutralSlots[neutralPlayerCount].transform.position, // target
                        iconSpeed*menuTimeProvider.deltaTime // max displacement
                    );
                    neutralPlayerCount++;
                    break;
            }
        }
    }
    private void Update() 
    {
        MoveMenuIcons();
    }
}
