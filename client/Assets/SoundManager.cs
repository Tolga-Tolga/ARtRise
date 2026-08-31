using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [Header("Sounds")]
    [SerializeField] private AudioClip soundSuccess;
    [SerializeField] private AudioClip soundFail;
    [SerializeField] private AudioClip soundNewCard;
    [SerializeField] private AudioClip soundAttack;
    [SerializeField] private AudioClip soundDead;

    private void Awake()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundSuccess()
    {
        Play(soundSuccess);
    }

    public void PlaySoundFail()
    {
        Play(soundFail);
    }

    public void PlaySoundNewCard()
    {
        Play(soundNewCard);
    }

    public void PlaySoundAttack()
    {
        Play(soundAttack);
    }

        public void PlaySoundDead()
    {
        Play(soundDead);
    }

    private void Play(AudioClip clip)
    {
        if (audioSource == null || clip == null)
            return;

        audioSource.PlayOneShot(clip);
    }
}