using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioGroup", menuName = "ScriptableObjects/AudioGroup", order = 1)]
public class SO_AudioGroup : ScriptableObject
{
    public AudioClip[] audioClips;
    public AudioClip GetRandomClip()
    {
        return audioClips[Random.Range(0, Mathf.Clamp(audioClips.Length - 1, 0, 99))];
    }
}
