using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PunchTargetBehavior : MonoBehaviour
{
    public enum AttackTarget { head, body };
    [SerializeField] [Range(0, 2f)] float trackingSpeed;
    [SerializeField] TimeProvider timeProvider;
    public AttackTarget targeting;
    private Transform targetTransform;
    public SO_FighterConfig fighterConfig;
    private CombatBehavior opponentCombat;
    private void Awake()
    {
        FindOpponent();
    }
    public void FindOpponent()
    {
        // Find the opponent
        foreach (CombatBehavior cb in FindObjectsOfType<CombatBehavior>())
        {
            if (cb.fighterConfig.corner != fighterConfig.corner)
            {
                opponentCombat = cb;
                if(targeting == AttackTarget.head){targetTransform = opponentCombat.headTransform;}
                if(targeting == AttackTarget.body){targetTransform = opponentCombat.bodyTransform;}
            }
        }
    }
    private void TrackTarget()
    {
        transform.position = Vector3.MoveTowards(
            transform.position, 
            targetTransform.position,
            trackingSpeed * timeProvider.fixedDeltaTime
        );
    }
    private void FixedUpdate()
    {
        // track the target
        TrackTarget();
    }
}
