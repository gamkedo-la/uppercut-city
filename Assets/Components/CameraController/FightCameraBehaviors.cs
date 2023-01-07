using Cinemachine;
using UnityEngine;

public class FightCameraBehaviors : MonoBehaviour
{
    private CinemachineVirtualCamera vCamLiveFight;
    private CinemachineComposer vCamComposer;
    private CinemachineTransposer vCamTransposer;
    private GameObject fighterA;
    private GameObject fighterB;
    private void Awake() {
        // find the fighters
        fighterA = GameObject.FindWithTag("FighterA");
        fighterB = GameObject.FindWithTag("FighterB");
        vCamLiveFight = GetComponent<CinemachineVirtualCamera>();
        vCamComposer = vCamLiveFight.GetCinemachineComponent<CinemachineComposer>();
        vCamTransposer = vCamLiveFight.GetCinemachineComponent<CinemachineTransposer>();
    }
    private void SetTrackingOffset(){
        vCamComposer.m_TrackedObjectOffset.x = (fighterA.transform.position - fighterB.transform.position).magnitude /-2;
    }
    private void SetFollowOffset(){
        vCamTransposer.m_FollowOffset.x = (fighterB.transform.position - fighterA.transform.position).magnitude /2;
        vCamTransposer.m_FollowOffset.z = (fighterB.transform.position - fighterA.transform.position).magnitude /2;
    }
    private void FixedUpdate() {
        // adjust 'look at' point
        SetTrackingOffset();
        SetFollowOffset();
    }
}
