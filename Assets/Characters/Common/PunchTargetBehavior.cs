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
        // resppond to events
        // when to switch from head to body
    }
    public void FindOpponent()
    {
        // Find the opponent
        foreach (CombatBehavior fb in FindObjectsOfType<CombatBehavior>())
        {
            if (fb.fighterConfig.corner != fighterConfig.corner)
            {
                opponentCombat = fb;
                targetTransform = targeting == AttackTarget.head ? opponentCombat.hitHeadDetector.transform : opponentCombat.hitBodyDetector.transform;
            }
        }
    }
    private void Start()
    {
        FindOpponent();
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
