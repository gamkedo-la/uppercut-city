using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenu : MonoBehaviour
{
    public SO_FighterConfig blueFighterConfig;
    public SO_FighterConfig redFighterConfig;
    public GameObject redWin;
    public GameObject blueWin;
    public GameObject tie;
    private void Awake()
    {
        Smb_Gs_MoveToCenter.onStateEnter += ResetEndSequence;
        Smb_Gs_DeclareWinner.onStateEnter += DeclareWinner;
    }
    private void OnEnable()
    {
        ResetEndSequence();
    }
    private void ResetEndSequence()
    {
        redWin.SetActive(false);
        blueWin.SetActive(false);
        tie.SetActive(false);
    }
    private void DeclareWinner()
    {
        if(blueFighterConfig.healthCurrent > redFighterConfig.healthCurrent)
        {
            blueWin.SetActive(true);
        }
        else if(blueFighterConfig.healthCurrent == redFighterConfig.healthCurrent)
        {
            tie.SetActive(true);
        }
        else
        {
            redWin.SetActive(true);
        }
    }
}
