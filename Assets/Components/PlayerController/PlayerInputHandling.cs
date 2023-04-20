using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputHandling : MonoBehaviour
{
    private SO_FighterConfig fighterConfig; // the fighter we are mapped to
    private PlayerController controller;
    private Vector2 movementInput;
    private Vector2 rightAnalogInput;
    private Vector2 punchInput;
    private double mousePunchAxis;
    private FighterBehaviors fighterBehaviors;
    public static PlayerController.PlaterControllerEvent onMenuPressed;
    // connect this object to a fighter
    // input logic goes in here
    // behaviours are handled in FighterBehaviors
    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        Smb_MatchLive.onStateEnter += LinkToFighter;
        PlayerController.newPlayerJoined += LinkToFighter;
        MenuManager.resumeGame += LinkToFighter;
        MenuManager.acceptCharacters += LinkToFighter;
    }
    private void GetFighterComponents(SO_FighterConfig.Corner corner)
    {
        foreach (FighterSetup fs in FindObjectsOfType<FighterSetup>())
        {
            if(fs.fighterConfig.corner == corner)
            {
                fighterConfig = fs.fighterConfig;
                fighterBehaviors = fs.GetComponent<FighterBehaviors>();
            }
        }
    }
    public void LinkToFighter()
    {
        switch (controller.playerConfig.allegiance)
        {
            case SO_PlayerConfig.Allegiance.neutral:
                fighterConfig = null;
                fighterBehaviors = null;
                break;
            case SO_PlayerConfig.Allegiance.red:
                GetFighterComponents(SO_FighterConfig.Corner.red);
                break;
            case SO_PlayerConfig.Allegiance.blue:
                GetFighterComponents(SO_FighterConfig.Corner.blue);
                break;
            default:
                break;
        }
    }
    public void HandleLeanModifier(InputAction.CallbackContext context)
    {
        fighterBehaviors?.SetLeanModifier(context.ReadValue<float>() > 0);
    }
    public void HandleBlockModifier(InputAction.CallbackContext context)
    {
        fighterBehaviors?.SetBlockModifier(context.ReadValue<float>() > 0);
    }
    public void HandleMenu(InputAction.CallbackContext context)
    {
        Debug.Log($"Request menu");
        onMenuPressed?.Invoke();
    }
    public void InputMovement(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        fighterBehaviors?.SetMovementVector(movementInput);
    }
    public void HandlePunchInput(InputAction.CallbackContext context)
    {
        punchInput = context.ReadValue<Vector2>();
        fighterBehaviors?.HandlePunch(punchInput);
    }
    public void InputMousePunch(InputAction.CallbackContext context)
    {
        punchInput.x = context.ReadValue<float>();
        punchInput.y = 0;
        fighterBehaviors?.HandlePunch(punchInput);
    }
    private void Start()
    {
        LinkToFighter();
    }
}
