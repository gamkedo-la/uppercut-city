using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdManager : MonoBehaviour
{

    public static CrowdManager Instance;

    public const float EXCITED_STATE_LOWER_LIMIT = 0.2f;
    public const float CELEBRATING_STATE_LOWER_LIMIT = 0.5f;
    public const float BLOODLUST_STATE_LOWER_LIMIT = 0.8f;
    

    CrowdState currentState;

    [SerializeField] float momentumDrainPerSecond = 0.1f;
    [SerializeField]  float punchThrownAdd = 0.005f;
    [SerializeField]  float punchConnectedAdd = 0.05f;
    float currentMomentum = 0f;

    bool isFightFinished = false;

    private enum CrowdState
    {
        Relaxed,
        Excited,
        Bloodlust,
        Celebrating
    }

    public float CurrentMomentum { get => currentMomentum; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Instance of CrowdManager already exists!");
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentState = CrowdState.Relaxed;
        SMB_CH_Followthrough.onStateEnter += FighterBehaviors_OnPunchThrown;
        PunchDetector.OnPunchConnected += PunchDetector_OnPunchConnected;
    }
    private void FighterBehaviors_OnPunchThrown()
    {
        currentMomentum += punchThrownAdd;
    }
    private void PunchDetector_OnPunchConnected(object sender, EventArgs e)
    {
        currentMomentum += punchConnectedAdd;
    }
    // Update is called once per frame
    void Update()
    {

        currentMomentum -= momentumDrainPerSecond * Time.deltaTime;
        currentMomentum = Mathf.Clamp01(currentMomentum);
        
        CheckCrowdState();

    }

    public void CheckCrowdState()
    {
        if (isFightFinished)
        {
            currentState = CrowdState.Celebrating;
        }
        switch (currentState)
        {
            case CrowdState.Relaxed:
                if (currentMomentum >= EXCITED_STATE_LOWER_LIMIT)
                {
                    currentState = CrowdState.Excited;
                }
                break;
            case CrowdState.Excited:
                if (currentMomentum < EXCITED_STATE_LOWER_LIMIT)
                {
                    currentState = CrowdState.Relaxed;
                    break;
                }
                if (currentMomentum >= BLOODLUST_STATE_LOWER_LIMIT)
                {
                    currentState = CrowdState.Bloodlust;
                }
                break;
            case CrowdState.Bloodlust:
                if (currentMomentum < BLOODLUST_STATE_LOWER_LIMIT)
                {
                    currentState = CrowdState.Excited;
                }
                break;
            case CrowdState.Celebrating:
                break;
        }
    }

    //Added for Testing purposes, feel free to delete at any time
    private void OnGUI() {
        GUI.Label (new Rect(0,0,100,20), "" + currentState);
        GUI.Label (new Rect(0,20,100, 20), "" + currentMomentum);
    }
}
