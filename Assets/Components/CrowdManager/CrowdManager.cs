using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdManager : MonoBehaviour
{

    AbstractCrowdState currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = new ExcitedState();
        currentState.OnStateEnter();

    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnStateUpdate();
    }

    void ChangeCrowdState(AbstractCrowdState prev, AbstractCrowdState next){
        prev.OnStateExit();
        currentState = next;
        currentState.OnStateEnter();
    }
}
