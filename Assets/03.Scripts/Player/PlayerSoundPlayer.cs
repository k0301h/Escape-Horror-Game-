using UnityEngine;

public class PlayerSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource rightFootSound;
    [SerializeField] private AudioSource leftFootSound;
    [SerializeField] private AudioSource BreathSound;

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
            BreathSound.Play();
        }
    }
}