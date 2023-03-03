using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchTargetBehavior : MonoBehaviour
{
    private enum TrackingSetting { head, body };
    [SerializeField] [Range(0, 2f)] float trackingSpeed;
    [SerializeField] TimeProvider timeProvider;
    private TrackingSetting tracking = TrackingSetting.head;
    public SO_FighterConfig fighterConfig;
    // Track either the head or the
    private FighterBehaviors opponent;
    private void Awake()
    {
        // resppond to events
        // when to switch from head to body    
    }
    private void Start()
    {
        // Find the opponent
        foreach (FighterBehaviors fb in FindObjectsOfType<FighterBehaviors>())
        {
            if (fb.fighterConfig.corner != fighterConfig.corner)
            {
                opponent = fb;
            }
        }
    }
    private void TrackTarget()
    {
        if(opponent != null)
        {
            switch (tracking)
            {
                case TrackingSetting.head:
                    transform.position = Vector3.MoveTowards(
                        transform.position, 
                        opponent.head.transform.position, 
                        trackingSpeed * timeProvider.fixedDeltaTime
                    );
                    break;
                case TrackingSetting.body:
                    break;
                default:
                    break;
            }
            
        }
    }
    private void FixedUpdate()
    {
        // track the target
        TrackTarget();
    }
}
