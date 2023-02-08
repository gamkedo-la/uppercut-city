using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterSfxController : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] SO_AudioGroup punchSounds;
    // subscribe to events in the player animator
    // play the sound from the audio source
    // audiosource.PlayOneShot(punchSounds.GetRandomClip());
}
