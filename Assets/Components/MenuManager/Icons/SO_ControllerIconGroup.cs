using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ControllerIcons", menuName = "ScriptableObjects/ControllerIcons", order = 1)]
public class SO_ControllerIconGroup : ScriptableObject
{
    public GameObject keyboardMouse;
    public GameObject genericGamepad;
    public GameObject playStation;
    public GameObject xBox;
}
