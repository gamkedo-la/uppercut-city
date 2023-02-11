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
        MenuManager.StartGame += HandleStartGame;
    }
    public void JoinNewPlayer(PlayerInput playerInput){
        Debug.Log($"New Player: {playerInput.currentControlScheme}");
        // playerInput.gameObject.GetComponent<PlayerConfig>().allegiance = PlayerConfig.Allegiance.neutral;
        // playerInput.gameObject.GetComponent<PlayerConfig>().SetFighterInput(fighterAInput);
        //playerInput.GetComponent<MultiplayerEventSystem>().firstSelectedGameObject = mainMenu.GetComponent<MainMenuScript>().currentItem;
        // configure input
    }
    private void HandleStartGame(object sender, System.EventArgs e){
        masterStateMachine.SetBool("MatchStarted", true);
    }
    void Update()
    {
        
    }
}
