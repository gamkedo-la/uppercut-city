using Cinemachine;
using UnityEngine;

public class FightCameraBehaviors : MonoBehaviour
{
    private CinemachineVirtualCamera vCamLiveFight;
    private CinemachineComposer vCamComposer;
    private CinemachineTransposer vCamTransposer;
    private FighterSetup[] fighters;
    private GameObject fighterB;
    private void Awake() {
        // find the fighters
        fighters = FindObjectsOfType<FighterSetup>();
        vCamLiveFight = GetComponent<CinemachineVirtualCamera>();
        vCamComposer = vCamLiveFight.GetCinemachineComponent<CinemachineComposer>();
        vCamTransposer = vCamLiveFight.GetCinemachineComponent<CinemachineTransposer>();
    }
    private void SetTrackingOffset(){
        vCamComposer.m_TrackedObjectOffset.z = (fighters[0].transform.position - fighters[1].transform.position).magnitude /2;
    }
    private void SetFollowOffset(){
        vCamTransposer.m_FollowOffset.x = (fighters[1].transform.position - fighters[0].transform.position).magnitude / 1.75f;
        vCamTransposer.m_FollowOffset.z = (fighters[1].transform.position - fighters[0].transform.position).magnitude /2;
    }
    private void FixedUpdate() {
        // adjust 'look at' point
        SetTrackingOffset();
        SetFollowOffset();
    }
}
