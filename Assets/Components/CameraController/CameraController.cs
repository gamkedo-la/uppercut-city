using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera vCamOverhead;
    [SerializeField] CinemachineVirtualCamera vCamLiveFight;
    void Start()
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
        vCamLiveFight.gameObject.SetActive(false);
    }
}
