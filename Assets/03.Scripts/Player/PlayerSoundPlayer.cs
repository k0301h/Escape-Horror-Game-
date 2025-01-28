using System.Collections;
using UnityEngine;

public class PlayerSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource rightFootSound;
    [SerializeField] private AudioSource leftFootSound;
    [SerializeField] private AudioSource inhaleBreathSound;
    [SerializeField] private AudioSource exhaleBreathSound;

    void Start()
    {
        StartCoroutine(BreathCorutine());
    }

    IEnumerator BreathCorutine()
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
        else if (soundName == "inhaleBreath")
        {
            inhaleBreathSound.Play();
        }
        else if (soundName == "exhaleBreath")
        {
            exhaleBreathSound.Play();
        }
    }
}