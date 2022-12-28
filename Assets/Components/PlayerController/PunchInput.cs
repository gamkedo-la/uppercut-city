using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PunchInput : MonoBehaviour
{
    private Vector2 punchInput;
    private GameObject fighter;
    private Animator fighterAnimator;
    private void Awake() {
        fighter = GameObject.FindWithTag("FighterB");
        //fighterAnimator = fighter.GetComponent<Animator>();
    }
    public void HandlePunchInput(InputAction.CallbackContext context){
        if(context.performed){
            Debug.Log("Punch");
            //fighterAnimator.SetTrigger("Jab");
        }
    }
}
