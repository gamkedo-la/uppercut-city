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
    public Mesh characterMesh;
    // Materials
    public Material mat_Skin;
    public Material mat_Head;
    public Material mat_Shoes;
    public Material mat_Shorts;
    public Material mat_Gloves;
}