using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    Dictionary<string, AbstractAIState> stateDictionary;
    private AbstractAIState currentState;
    private void Awake() {
        stateDictionary = new Dictionary<string, AbstractAIState>();
        stateDictionary.Add(AIS_Idle.stateName, new AIS_Idle(this));
        stateDictionary.Add(AIS_Attack.stateName, new AIS_Attack(this));
        stateDictionary.Add(AIS_UserControl.stateName, new AIS_UserControl(this));
        currentState = stateDictionary[AIS_Idle.stateName];
        // Subscribe to events
    }
    private void OnEnable()
    {
        Smb_MatchLive.onStateEnter += Ev_FightStart;
    }
    private void OnDisable() {
        
    }
    private void Ev_FightStart()
    {
        ChangeState(AIS_Attack.stateName);
    }
    public void ChangeState(string newState)
    {
        currentState.OnExit();
        print($"{gameObject.name} changed from {currentState} to {newState}");
        if (stateDictionary.TryGetValue(newState, out AbstractAIState state))
        {
            currentState = state;
            currentState.OnEnter();
            return;
        }
    }
    private void Start() {
        currentState?.OnEnter();
    }
    private void FixedUpdate() {
        currentState.OnUpdate();
    }
}
