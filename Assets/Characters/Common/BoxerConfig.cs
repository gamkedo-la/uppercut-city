using UnityEngine;
[CreateAssetMenu(fileName = "BoxerConfig", menuName = "ScriptableObjects/BoxerConfig", order = 1)]
public class BoxerConfig : ScriptableObject
{
    // data that control the character
    public float movementSpeed;
    public float rotationSpeed;
    public float jabPower;
    public float hookPower;
    public float uppercutPower;
    public float temporaryDamageLimit;
    public float stamina;
    public float fatigue;
}