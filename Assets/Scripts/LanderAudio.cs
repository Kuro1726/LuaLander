using System;
using UnityEngine;

public class LanderAudio : MonoBehaviour
{
    [SerializeField] private AudioSource thrusterAudioSource;

    void Awake()
    {
    }

    void Start()
    {
        Lander.Instance.OnBeforeForce += Lander_OnOnBeforeForce;
        Lander.Instance.OnUpForce += Lander_OnOnUpForce;
        Lander.Instance.OnLeftForce += Lander_OnOnLeftForce;
        Lander.Instance.OnRightForce += Lander_OnOnRightForce;
        thrusterAudioSource.Pause();
        SoundManager.Instance.onSoundChangeEvent += SoundManager_OnonSoundChangeEvent;
    }

    private void SoundManager_OnonSoundChangeEvent(object sender, EventArgs e)
    {
        thrusterAudioSource.volume = SoundManager.Instance.GetSoundVolumeNormalized();
    }

    private void Lander_OnOnUpForce(object sender, EventArgs e)
    {
        if (!thrusterAudioSource.isPlaying)
        {
            thrusterAudioSource.Play();
        }
    }

    private void Lander_OnOnRightForce(object sender, EventArgs e)
    {
        if (!thrusterAudioSource.isPlaying)
        {
            thrusterAudioSource.Play();
        }
    }

    private void Lander_OnOnLeftForce(object sender, EventArgs e)
    {
        if (!thrusterAudioSource.isPlaying)
        {
            thrusterAudioSource.Play();
        }
    }

    private void Lander_OnOnBeforeForce(object sender, EventArgs e)
    {
        thrusterAudioSource.Pause();
    }
}
