using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PunchTargetBehavior : MonoBehaviour
{
    public enum AttackTarget { headLeft, headRight, bodyLeft, bodyRight };
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
                switch (targeting)
                {
                    case AttackTarget.headLeft:
                        targetTransform = opponentCombat.headHitLeft.transform;
                        break;
                    case AttackTarget.headRight:
                        targetTransform = opponentCombat.headHitRight.transform;
                        break;
                    case AttackTarget.bodyLeft:
                        targetTransform = opponentCombat.bodyHitLeft.transform;
                        break;
                    case AttackTarget.bodyRight:
                        targetTransform = opponentCombat.bodyHitRight.transform;
                        break;
                    default:
                        break;
                }
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
