using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] footStepClips;
    [SerializeField] private float stepInterval = 0.5f;

    private float stepTimer;


    private void Update() {
        if (characterController.isGrounded && characterController.velocity.magnitude > 0.5f) {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f) {
                PlayFootSteps();
                stepTimer = stepInterval;
            }
        }
        else {
            stepTimer = 0f;
        }
    }

    private void PlayFootSteps() {
        if (footStepClips.Length > 0) {
            AudioClip clip = footStepClips[Random.Range(0, footStepClips.Length)];
            audioSource.PlayOneShot(clip);
        }
    }




}
