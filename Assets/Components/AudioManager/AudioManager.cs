using UnityEngine;
public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    public SO_AudioGroup menuMusic;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        audioSource.PlayOneShot(menuMusic.GetRandomClip());
        audioSource.loop = true;
    }
}