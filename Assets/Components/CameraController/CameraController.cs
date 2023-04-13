using Cinemachine;
using UnityEngine;
public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera vCamOverhead;
    [SerializeField] CinemachineVirtualCamera vCamStareDown;
    [SerializeField] CinemachineVirtualCamera vCamLiveFight;
    private void Awake()
    {
        StateGameSetup.onStateEnter += SwitchToStareDown;
        Smb_MatchLive.onStateEnter += HandleFightLive;
        StateFightersToCorners.onStateEnter += SwitchToOverheadCam;
    }
    private void SwitchToStareDown()
    {
        DisableAllCams();
        vCamStareDown.gameObject.SetActive(true);
    }
    private void HandleFightLive()
    {
        DisableAllCams();
        vCamLiveFight.gameObject.SetActive(true);
    }
    private void SwitchToOverheadCam()
    {
        DisableAllCams();
        vCamOverhead.gameObject.SetActive(true);
    }
    private void DisableAllCams()
    {
        vCamOverhead.gameObject.SetActive(false);
        vCamStareDown.gameObject.SetActive(false);
        vCamLiveFight.gameObject.SetActive(false);
    }
}
