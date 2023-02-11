using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem.UI;
using UnityEngine;

//Respond to events handle components
public class PlayerController : MonoBehaviour
{
    [SerializeField] MultiplayerEventSystem mpEventSystem;
    [SerializeField] InputSystemUIInputModule mpUiInputModule;
    private void Awake() {
        // subscribe to events for state changes
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
