using UnityEngine;

public class SoundPlayer : MonoBehaviour {

    private AudioSource _audioSource;
    public AudioClip death;
    public AudioClip hit;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayHitSound()
    {
        if (_audioSource.clip != hit)
        {
            _audioSource.clip = hit;
        }

        _audioSource.volume = Settings.Volume;
        _audioSource.Play();
    }

    public void PlayDeathSound()
    {
        if (_audioSource.clip != death)
        {
            _audioSource.clip = death;
        }

        _audioSource.volume = Settings.Volume;
        _audioSource.Play();
    }
}
