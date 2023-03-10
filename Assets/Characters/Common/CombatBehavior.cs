using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CombatBehavior : MonoBehaviour
{
    public GameObject rightHand;
    public GameObject leftHand;
    //damage dealer class
    private Collider rightHandCollider;
    private Collider leftHandCollider;
    private Animator animator;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rightHandCollider = rightHand.GetComponent<Collider>();
        leftHandCollider = leftHand.GetComponent<Collider>();
    }
    public void DisablePunches()
    {
        rightHand.SetActive(false);
        leftHand.SetActive(false);
    }
    public void EnablePunch(SMB_CH_Followthrough.PunchHand punchHand)
    {
        if(punchHand == SMB_CH_Followthrough.PunchHand.right)
        {
            rightHand.SetActive(true);
            return;
        }
        if(punchHand == SMB_CH_Followthrough.PunchHand.left)
        {
            leftHand.SetActive(true);
            return;
        }
    }
}
