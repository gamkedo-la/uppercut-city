using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputHandling : MonoBehaviour
{
    private PlayerController controller;
    private PlayerInput playerInput;
    private SO_InputData so_fighterInput;
    private Camera mainCamera;
    private Vector2 movementInput;
    private Vector2 punchInput;
    private FighterBehaviors fighterBehaviors;
    private Animator fighterAnimator;
    // connect this object to a fighter
    // input logic goes in here
    // behaviours are handled in FighterBehaviors
    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        playerInput = GetComponent<PlayerInput>();
        mainCamera = Camera.main;
        Smb_MatchLive.onStateEnter += Ev_FightStart;
    }
    public void Ev_FightStart(object sender, System.EventArgs e){
        if(controller.playerConfig.allegiance == SO_PlayerConfig.Allegiance.red){
            so_fighterInput = controller.playerConfig.inputRedFighter;
            Debug.Log($"controlling {controller.playerConfig.inputRedFighter}");
            return;
        }
        if(controller.playerConfig.allegiance == SO_PlayerConfig.Allegiance.blue){
            so_fighterInput = controller.playerConfig.inputRedFighter;
            Debug.Log($"controlling {so_fighterInput}");
            return;
        }
    }
    public void HandleLeanModifier(InputAction.CallbackContext context){
        if(context.performed){

        }
        Debug.Log($"leaning {context.ReadValue<float>() > 0}");
        //fighterBehaviors.SetLeanModifier(context.ReadValue<float>() > 0);
        // so_fighterInput.leanModifier 
    }
    public void InputMovement(InputAction.CallbackContext context){
        movementInput = context.ReadValue<Vector2>();
        // camera relative movement - take movement input and rotate around vertical axis by i
        // i being the camera's rotation 
        // mainCamera.transform.rotation.y
        //fighterBehaviors.SetMovementVector(movementInput);
    }
    public void HandlePunchInput(InputAction.CallbackContext context){
        punchInput = context.ReadValue<Vector2>();
        //fighterBehaviors.HandlePunch(Mathf.Atan2(punchInput.x, punchInput.y) * Mathf.Rad2Deg);
    }
    private void Start() {
        // Assign this player controller to a fighter
        // This is what the 'ChooseSides' menu scripting will do later on
        // ITMT: just grab player A
    }
}
