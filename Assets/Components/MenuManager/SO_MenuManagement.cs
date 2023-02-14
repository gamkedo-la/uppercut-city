using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MenuState", menuName = "ScriptableObjects/MenuState", order = 1)]
public class SO_MenuManagement : ScriptableObject
{
    public GameObject currentlyActiveItem;
}
