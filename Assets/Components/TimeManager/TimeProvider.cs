using UnityEngine;

[CreateAssetMenu(fileName = "TimeProvider", menuName = "ScriptableObjects/TimeProvider", order = 1)]
public class TimeProvider : ScriptableObject
{
    public float time;
    public float deltaTime;
    public float fixedDeltaTime;
    [Range(0.02f, 3f)] public float timeScale;
}
