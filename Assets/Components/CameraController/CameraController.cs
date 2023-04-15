using Cinemachine;
using UnityEngine;
public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera vCamOverhead;
    public CinemachineVirtualCamera vCamStareDown;
    public CinemachineVirtualCamera vCamLiveFight;
    public CinemachineVirtualCamera vCamKnockout;
    private void Awake()
    {
        StateGameSetup.onStateEnter += SwitchToStareDown;
        Smb_MatchLive.onStateEnter += HandleFightLive;
        StateFightersToCorners.onStateEnter += SwitchToOverheadCam;
        SO_FighterConfig.onFighterDown += SwitchToKnockoutCam;
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
    private void SwitchToKnockoutCam()
    {
        DisableAllCams();
        vCamKnockout.gameObject.SetActive(true);
    }
    private void DisableAllCams()
    {
        vCamOverhead.gameObject.SetActive(false);
        vCamStareDown.gameObject.SetActive(false);
        vCamLiveFight.gameObject.SetActive(false);
        vCamKnockout.gameObject.SetActive(false);
    }
}
