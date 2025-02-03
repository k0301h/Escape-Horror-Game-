using System.Collections;
using UnityEngine;

public class PlayerSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource rightFootSound;
    [SerializeField] private AudioSource leftFootSound;
    [SerializeField] private AudioSource inhaleBreathSound;
    [SerializeField] private AudioSource exhaleBreathSound;
    
    private Coroutine _breathCoroutine;
    
    IEnumerator BreathCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        float volume = 0.3f;
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
         
            inhaleBreathSound.volume = volume;
            exhaleBreathSound.volume = volume;
            
            DebugSound(inhaleBreathSound, true);
            SoundManager.Instance?.AudioPlay(inhaleBreathSound);
            yield return new WaitForSeconds(1.0f);
            
            DebugSound(exhaleBreathSound, true);
            SoundManager.Instance?.AudioPlay(exhaleBreathSound);
            yield return new WaitForSeconds(1.5f);
            
            if(volume < 0.5f)
                volume += 0.05f;
        }
    }
    
    public void PlaySound(string soundName)
    {
        if (soundName == "rightFoot")
        {
            DebugSound(rightFootSound, true);
            SoundManager.Instance?.AudioPlay(rightFootSound);
        }
        else if (soundName == "leftFoot")
        {
            DebugSound(leftFootSound, true);
            SoundManager.Instance?.AudioPlay(leftFootSound);
        }
        else if (soundName == "Breath")
        {
            _breathCoroutine = StartCoroutine(BreathCoroutine());
        }
    }

    public void StopSound(string soundName)
    {
        if (soundName == "rightFoot")
        {
            DebugSound(rightFootSound, false);
            SoundManager.Instance?.AudioStop(rightFootSound);
        }
        else if (soundName == "leftFoot")
        {
            DebugSound(leftFootSound, false);
            SoundManager.Instance?.AudioStop(leftFootSound);
        }
        else if (soundName == "Breath")
        {
            StopCoroutine(_breathCoroutine);
        }
    }
    
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    private void DebugSound(AudioSource audioSource, bool isPlay)
    {
        #if UNITY_EDITOR
        if (SoundManager.Instance == null)
        {
            if(isPlay)
                audioSource.Play();
            else
                audioSource.Stop();
        }
        #endif
    }
}