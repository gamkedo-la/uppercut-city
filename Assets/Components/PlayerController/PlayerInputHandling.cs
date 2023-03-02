using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputHandling : MonoBehaviour
{
    private SO_FighterConfig fighterConfig;
    private PlayerController controller;
    private SO_FighterControlData so_fighterInput;       
    private Camera mainCamera;
    private Vector2 movementInput;
    private Vector2 punchInput;
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
    private void GetFighterConfig(SO_FighterConfig.Corner corner)
    {
        // Get a reference to the fighterconfig for that corner
        foreach (FighterSetup fs in FindObjectsOfType<FighterSetup>())
        {
            if(fs.fighterConfig.corner == corner)
            {
                fighterConfig = fs.fighterConfig;
            }
            else
            {
                fighterConfig.opponentConfig = fs.fighterConfig;
            }
        }
    }
    public void LinkToFighter()
    {
        if(controller.playerConfig.allegiance == SO_PlayerConfig.Allegiance.red)
        {
            GetFighterConfig(SO_FighterConfig.Corner.red);
            Debug.Log($"controlling {controller.playerConfig.inputRedFighter}");
            return;
        }
        if(controller.playerConfig.allegiance == SO_PlayerConfig.Allegiance.blue)
        {
            GetFighterConfig(SO_FighterConfig.Corner.blue);
            Debug.Log($"controlling {controller.playerConfig.inputBlueFighter}");
            return;
        }
    }
    public void Ev_FightStart(object sender, System.EventArgs e)
    {
        LinkToFighter();
    }
    public void HandleLeanModifier(InputAction.CallbackContext context)
    {
        Debug.Log($"leaning {context.ReadValue<float>() > 0}");
        fighterBehaviors?.SetLeanModifier(context.ReadValue<float>() > 0);
        // so_fighterInput.leanModifier 
    }
    public void HandleMenu(InputAction.CallbackContext context)
    {
        controller.playerConfig.playerInput.SwitchCurrentActionMap("UI");
        onMenuPressed?.Invoke(this, EventArgs.Empty);
    }
    public void InputMovement(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        // camera relative movement - take movement input and rotate around vertical axis by i
        // i being the camera's rotation 
        // mainCamera.transform.rotation.y
        fighterBehaviors?.SetMovementVector(movementInput);
    }
    public void HandlePunchInput(InputAction.CallbackContext context){
        punchInput = context.ReadValue<Vector2>();
        fighterBehaviors?.HandlePunch(Mathf.Atan2(punchInput.x, punchInput.y) * Mathf.Rad2Deg);
    }
    private void Start() {
        // Assign this player controller to a fighter
        // This is what the 'ChooseSides' menu scripting will do later on
        // ITMT: just grab player A
        LinkToFighter();
    }
}
