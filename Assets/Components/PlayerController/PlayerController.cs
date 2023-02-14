using System;
using UnityEngine.InputSystem.UI;
using UnityEngine;

//Respond to events handle components
public class PlayerController : MonoBehaviour
{
    [SerializeField] MultiplayerEventSystem mpEventSystem;
    [SerializeField] InputSystemUIInputModule mpUiInputModule;
    public SO_PlayerConfig playerConfig;
    private void Awake() {
        // subscribe to events for state changes
        playerConfig = ScriptableObject.CreateInstance<SO_PlayerConfig>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
