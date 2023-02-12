using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
public class GameSystem : MonoBehaviour
{
    public Animator masterStateMachine;
    public SO_GameType[] gameTypes;
    public SO_InputData fighterAInput;
    public SO_InputData fighterBInput;
    private void Awake(){
        masterStateMachine = GetComponent<Animator>();
        MenuManager.setupMatch += HandleSetupMatch;
    }
    public void JoinNewPlayer(PlayerInput playerInput){
        Debug.Log($"New Player: {playerInput.currentControlScheme}");
    }
    private void HandleSetupMatch(object sender, System.EventArgs e){
        //masterStateMachine.SetBool("MatchStarted", true);
    }
    private void HandleStartMatch(object sender, System.EventArgs e){
        masterStateMachine.SetBool("MatchStarted", true);
    }
    void Update()
    {
        
    }
}
