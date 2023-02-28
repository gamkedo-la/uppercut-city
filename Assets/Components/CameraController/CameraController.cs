using Cinemachine;
using UnityEngine;
public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera vCamOverhead;
    [SerializeField] CinemachineVirtualCamera vCamStareDown;
    [SerializeField] CinemachineVirtualCamera vCamLiveFight;
    private void Awake()
    {
        StateLiveFightCam.onStateEnter += HandleFightLive;    
    }
    private void HandleFightLive(object sender, System.EventArgs e)
    {
        DisableAllCams();
        vCamLiveFight.gameObject.SetActive(true);
    }
    private void DisableAllCams()
    {
        vCamOverhead.gameObject.SetActive(false);
        vCamStareDown.gameObject.SetActive(false);
        vCamLiveFight.gameObject.SetActive(false);
    }
}
