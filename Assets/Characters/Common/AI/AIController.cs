using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AIState
{
    Idle,
    Chase,
    Attack,
    Flee
}

public class StateMachineAI : MonoBehaviour
{
    public AIState initialState = AIState.Idle;
    public Transform playerTransform;
    public float moveSpeed = 5f;
    public float attackRange = 2f;
    public float fleeDistance = 10f;

    private AIState currentState;

    private void Start()
    {
        // Set the initial state of the AI
        currentState = initialState;
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case AIState.Idle:
                if (CanSeePlayer())
                {
                    currentState = AIState.Chase;
                }
                break;
            case AIState.Chase:
                if (IsInRangeToAttack())
                {
                    currentState = AIState.Attack;
                }
                else if (!CanSeePlayer())
                {
                    currentState = AIState.Idle;
                }
                else if (IsTooFarToChase())
                {
                    currentState = AIState.Flee;
                }
                else
                {
                    Chase();
                }
                break;
            case AIState.Attack:
                if (!IsInRangeToAttack())
                {
                    currentState = AIState.Chase;
                }
                else
                {
                    Attack();
                }
                break;
            case AIState.Flee:
                if (IsSafeDistance())
                {
                    currentState = AIState.Idle;
                }
                else
                {
                    Flee();
                }
                break;
        }
    }

    private bool CanSeePlayer()
    {
        // Implement the logic to detect if the player is within the AI's line of sight
        // and return true or false
        return false;
    }

    private bool IsInRangeToAttack()
    {
        // Implement the logic to detect if the player is within attack range
        // and return true or false
        return false;
    }

    private bool IsTooFarToChase()
    {
        // Implement the logic to detect if the player is too far away to chase
        // and return true or false
        return false;
    }

    private bool IsSafeDistance()
    {
        // Implement the logic to detect if the AI has fled a safe distance away
        // and return true or false
        return false;
    }

    private void Chase()
    {
        // Implement the logic to move towards the player
        Vector3 direction = (playerTransform.position - transform.position).normalized;
    }

    private void Attack()
    {
        // Implement the logic to attack the player
        Debug.Log("Attacking!");
    }

    private void Flee()
    {
        // Implement the logic to flee from the player
        Vector3 direction = (transform.position - playerTransform.position).normalized;
    }
}
