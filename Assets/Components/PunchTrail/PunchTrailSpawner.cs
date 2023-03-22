using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchTrailSpawner : MonoBehaviour
{

    [SerializeField] PunchTrail punchTrailPrefab;
    [SerializeField] Transform target;
    [SerializeField] Transform rightOrigin;
    [SerializeField] Transform leftOrigin;
    [SerializeField] float rightSpeed = 5f;
    [SerializeField] float rightDistance = 0.4f;
    [SerializeField] float leftSpeed = 5f;
    [SerializeField] float leftDistance = 0.8f;


    // Start is called before the first frame update
    void Start()
    {
        CombatBehavior combatBehavior = transform.parent.GetComponentInChildren<CombatBehavior>();

        combatBehavior.onRightPunchThrown += CombatBehavior_RightPunchThrown;

        combatBehavior.onLeftPunchThrown += CombatBehavior_LeftPunchThrown;

    }

    private void CombatBehavior_LeftPunchThrown(object sender, EventArgs e)
    {
        PunchThrown(leftOrigin.position, leftSpeed, leftDistance);
    }

    private void CombatBehavior_RightPunchThrown(object sender, EventArgs e)
    {
        PunchThrown(rightOrigin.position, rightSpeed, rightDistance);
    }
    
    private void PunchThrown(Vector3 originPosition, float speed, float distance){
        var trail = Instantiate(punchTrailPrefab);
        trail.Setup(originPosition, target.position, speed, distance);
    }

}
