using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioGroup", menuName = "ScriptableObjects/AudioGroup", order = 1)]
public class SO_AudioGroup : ScriptableObject
{
    public List<AudioClip> audioClips;
    public AudioClip GetRandomClip()
    {
        return audioClips[Random.Range(0, audioClips.Count)];
    }
}
