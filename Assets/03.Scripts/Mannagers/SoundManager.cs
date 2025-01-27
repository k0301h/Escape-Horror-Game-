using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public void AudioPlay(AudioSource audioSource)
    {
        audioSource.Play();
    }

    public void AudioStop(AudioSource audioSource)
    {
        audioSource.Stop();
    }
}
