using System.Collections;
using UnityEngine;

public class PlayerSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource rightFootSound;
    [SerializeField] private AudioSource leftFootSound;
    [SerializeField] private AudioSource inhaleBreathSound;
    [SerializeField] private AudioSource exhaleBreathSound;
    
    private Coroutine _breathCoroutine;

    void Start()
    {
        
    }
    
    IEnumerator BreathCoroutine()
    {
        while (true)
        {
            inhaleBreathSound.Play();
            yield return new WaitForSeconds(1.0f);
            exhaleBreathSound.Play();
            yield return new WaitForSeconds(1.5f);
        }
    }
    
    public void PlaySound(string soundName)
    {
        if (soundName == "rightFoot")
        {
            rightFootSound.Play();
        }
        else if (soundName == "leftFoot")
        {
            leftFootSound.Play();
        }
        else if (soundName == "Breath")
        {
            _breathCoroutine = StartCoroutine(BreathCoroutine());
        }
    }

    public void StopSound(string soundName)
    {
        if (soundName == "Breath")
        {
            StopCoroutine(_breathCoroutine);
        }
    }
}