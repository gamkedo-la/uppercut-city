using System;
using System.Collections.Generic;
using UnityEngine;

public class FighterSetup : MonoBehaviour
{
    public SO_FighterConfig fighterConfig;
    public SO_UserProfile userProfile;
    public SkinnedMeshRenderer fighterMeshRenderer;
    private void SetMeshWithTextures(SO_Character character)
    {
        fighterConfig.activeCharacter = character;
        fighterMeshRenderer.sharedMesh = fighterConfig.activeCharacter.characterMesh;
        fighterMeshRenderer.materials = fighterConfig.activeCharacter.characterMaterials;
    }
    private void Awake()
    {
        if (fighterConfig.corner == SO_FighterConfig.Corner.red)
        {
            SetMeshWithTextures(userProfile.defaultRedFighter);
        }
        else if (fighterConfig.corner == SO_FighterConfig.Corner.blue)
        {
            SetMeshWithTextures(userProfile.defaultBlueFighter);
        }
        else
        {
            Debug.Log("BUG: A fighter has to be assigned to a corner");
        }
    }
}
