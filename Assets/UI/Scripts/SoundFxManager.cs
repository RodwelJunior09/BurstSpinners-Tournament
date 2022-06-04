using UnityEngine;

public class SoundFxManager : MonoBehaviour
{
    [Header("Sound FX")]
    [SerializeField] AudioClip _acceptSoundFx;
    [SerializeField] AudioClip _declineSoundFx;

    AudioSource _audioSource;

    private void Start() {
        this._audioSource = GetComponent<AudioSource>();
    }

    public void PlayAcceptSoundFx(){
        _audioSource.PlayOneShot(_acceptSoundFx);
    }

    public void PlayDeclineSoundFx(){
        _audioSource.PlayOneShot(_declineSoundFx);
    }
}
