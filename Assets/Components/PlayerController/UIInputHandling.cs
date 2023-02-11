using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
public class UIInputHandling : MonoBehaviour
{
    private MultiplayerEventSystem multiplayerEventSystem;
    private void Awake() {
        multiplayerEventSystem = GetComponent<MultiplayerEventSystem>();
        // fighter = GameObject.FindWithTag("FighterB");
    }
}
