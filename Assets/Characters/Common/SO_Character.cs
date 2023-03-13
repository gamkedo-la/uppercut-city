using UnityEngine;
[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/Character", order = 1)]
public class SO_Character : ScriptableObject
{
    // data that control the character
    public float movementSpeed;
    public float rotationSpeed;
    public float jabPower;
    public float crossPower;
    public float hookPower;
    public float uppercutPower;
    public float temporaryDamageLimit;
    public float healCooldown; // the delay between getting hit and beginning to heal again
    public float staminaCooldown;  // the delay between throwing a punch and beginning to regen stamina again
    public float healthRegenRate;
    public float staminaRegenRate;
    public Mesh characterMesh;
    // Materials
    public Material[] characterMaterials;
}