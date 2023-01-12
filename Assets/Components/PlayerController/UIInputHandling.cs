using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class UIInputHandling : MonoBehaviour
{
    private GameObject fighter;
    private Animator fighterAnimator;
    private void Awake() {
        fighter = GameObject.FindWithTag("FighterB");
    }
}
