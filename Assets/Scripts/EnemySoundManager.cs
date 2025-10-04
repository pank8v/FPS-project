using UnityEngine;

public class EnemySoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip attackClip;
    [SerializeField] private AudioClip[] footStepClips;

    public void PlayAttackCliP() {
        audioSource.PlayOneShot(attackClip);
    }
    
    
    
}
