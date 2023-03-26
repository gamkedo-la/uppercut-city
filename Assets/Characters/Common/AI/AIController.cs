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
        currentState = stateDictionary[AIS_Idle.stateName];
        // Subscribe to events
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
        currentState.OnEnter();
    }
    private void FixedUpdate() {
        currentState.OnUpdate();
    }
}
