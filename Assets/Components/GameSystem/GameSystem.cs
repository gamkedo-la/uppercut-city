using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public Animator masterStateMachine;
    private void Awake(){
        masterStateMachine = GetComponent<Animator>();
        MenuManager.StartGame += HandleStartGame;
    }
    private void HandleStartGame(object sender, System.EventArgs e){
        masterStateMachine.SetBool("MatchStarted", true);
    }
    void Update()
    {
        
    }
}
