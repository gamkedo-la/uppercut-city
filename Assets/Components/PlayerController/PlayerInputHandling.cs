using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputHandling : MonoBehaviour
{
    private SO_FighterConfig fighterConfig; // the fighter we are mapped to
    private PlayerController controller;
    private SO_FighterControlData so_fighterInput;
    private Camera mainCamera;
    private Vector2 movementInput;
    private Vector2 rightAnalogInput;
    private Vector2 punchInput;
    private double mousePunchAxis;
    private FighterBehaviors fighterBehaviors;
    public static EventHandler onMenuPressed;
    // connect this object to a fighter
    // input logic goes in here
    // behaviours are handled in FighterBehaviors
    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        mainCamera = Camera.main;
        Smb_MatchLive.onStateEnter += Ev_FightStart;
        PlayerController.newPlayerJoined += Ev_FightStart;
    }
    private void GetFighterBehaviors(SO_FighterConfig.Corner corner)
    {
        foreach (FighterBehaviors fb in FindObjectsOfType<FighterBehaviors>())
        {
            if(fb.fighterConfig.corner == corner)
            {
                fighterBehaviors = fb;
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
                GetFighterBehaviors(SO_FighterConfig.Corner.red);
                break;
            case SO_PlayerConfig.Allegiance.blue:
                GetFighterBehaviors(SO_FighterConfig.Corner.blue);
                break;
            default:
                break;
        }
        foreach (FighterSetup fs in FindObjectsOfType<FighterSetup>())
        {
            if(fs.fighterConfig.corner == SO_FighterConfig.Corner.red)
            {
                fighterConfig = fs.fighterConfig;
            }
            else
            {
                fighterConfig.opponentConfig = fs.fighterConfig;
            }
        }
    }
    public void Ev_FightStart()
    {
        LinkToFighter();
    }
    public void HandleLeanModifier(InputAction.CallbackContext context)
    {
        fighterBehaviors?.SetLeanModifier(context.ReadValue<float>() > 0);
        // so_fighterInput.leanModifier 
    }
    public void HandleBlockModifier(InputAction.CallbackContext context)
    {
        fighterBehaviors?.SetBlockModifier(context.ReadValue<float>() > 0);
    }
    public void HandleMenu(InputAction.CallbackContext context)
    {
        controller.playerConfig.playerInput.SwitchCurrentActionMap("UI");
        onMenuPressed?.Invoke(this, EventArgs.Empty);
    }
    public void InputMovement(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        fighterBehaviors?.SetMovementVector(movementInput);
    }
    public void HandlePunchInput(InputAction.CallbackContext context){
        punchInput = context.ReadValue<Vector2>();
        fighterBehaviors?.HandlePunch(punchInput);
    }
    public void InputMousePunch(InputAction.CallbackContext context){
        punchInput.x = context.ReadValue<float>();
        punchInput.y = 0;
        fighterBehaviors?.HandlePunch(punchInput);
    }
    public void HandleBlockRightInput(InputAction.CallbackContext context)
    {
        fighterBehaviors?.HandleBlock(FighterBehaviors.BlockType.right, context.ReadValue<float>() > 0);
    }
    public void HandleBlockLeftInput(InputAction.CallbackContext context)
    {
        fighterBehaviors?.HandleBlock(FighterBehaviors.BlockType.left, context.ReadValue<float>() > 0);
    }
    private void Start() {
        // Assign this player controller to a fighter
        // This is what the 'ChooseSides' menu scripting will do later on
        // ITMT: just grab player A
        LinkToFighter();
    }
}
