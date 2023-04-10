using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellRinger : MonoBehaviour
{
    public SO_SfxGroup sfxBellRings;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Smb_MatchLive.onStateEnter += Ev_BellRings;  
        Smb_MatchLive.onStateExit += Ev_BellRings;      
    }
    private void Ev_BellRings()
    {
        audioSource.PlayOneShot(sfxBellRings.GetRandomSfx());
    }
}
