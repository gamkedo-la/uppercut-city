using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ControllerIcons", menuName = "ScriptableObjects/ControllerIcons", order = 1)]
public class SO_ControllerIconGroup : ScriptableObject
{
    public Texture2D keyboardMouse;
    public Texture2D genericGamepad;
    public Texture2D playStation;
    public Texture2D xBox;
}
