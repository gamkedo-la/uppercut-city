using UnityEngine;
[CreateAssetMenu(fileName = "InputConfig", menuName = "ScriptableObjects/InputConfig", order = 1)]
public class InputConfig : ScriptableObject
{
    // data that control the character
    public enum Corner { red, blue, neutral };
}