using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ControllerIcons", menuName = "ScriptableObjects/ControllerIcons", order = 1)]
public class SO_ControllerIconGroup : ScriptableObject
{
    public Sprite keyboardMouse;
    public Sprite genericGamepad;
    public Sprite playStation;
    public Sprite xBox;
}
