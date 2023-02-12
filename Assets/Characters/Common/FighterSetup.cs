using System;
using System.Collections.Generic;
using UnityEngine;

public class FighterSetup : MonoBehaviour
{
    public enum Corner {red, blue};
    public Corner corner;
    public SO_UserProfile userProfile;
    public SkinnedMeshRenderer fighterMeshRenderer;
    public SO_Character activeCharacter;
    private void SetMeshWithTextures(SO_Character character)
    {
        activeCharacter = character;
        fighterMeshRenderer.sharedMesh = character.characterMesh;
        fighterMeshRenderer.materials = character.characterMaterials;
    }
    private void Awake() {
        if (corner == Corner.red)
        {
            SetMeshWithTextures(userProfile.defaultRedFighter);
        }
        else if (corner == Corner.blue)
        {
            SetMeshWithTextures(userProfile.defaultBlueFighter);
        }
        else
        {
            Debug.Log("No Corner Selected");
        }
    }
    public void ApplyConfigurations(object sender)
    {
        // trigger this to read data and apply it to the fighter
        // models, textures, etc.
    }
    void Start()
    {
        
    }
}
