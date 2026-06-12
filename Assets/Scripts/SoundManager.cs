using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip coinPickUpSound;
    [SerializeField] private AudioClip fuelPickUpSound;
    [SerializeField] private AudioClip crashSound;
    [SerializeField] private AudioClip landingSuccessSound;
    private static int soundVolume = 6;
    private const int SOUND_VOLUME_MAX = 10;
    public static SoundManager Instance;
    public event EventHandler onSoundChangeEvent;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Lander.Instance.OnPickupCoinEvent += Lander_OnOnPickupCoinEvent;
        Lander.Instance.OnPickupFuelEvent += Lander_OnOnPickupFuelEvent;
        Lander.Instance.OnLanded += Lander_OnOnLanded;
    }
    

    private void Lander_OnOnLanded(object sender, Lander.OnLandedEventArgs e)
    {
        switch (e.landingType)
        {
            case (Lander.LandingType.Success) :
                AudioSource.PlayClipAtPoint(landingSuccessSound, Camera.main.transform.position, GetSoundVolumeNormalized());
                break;
            case (Lander.LandingType.TooFastLanding) : case (Lander.LandingType.TooSteepAngle) : case (Lander.LandingType.WrongLandingArea) :
                AudioSource.PlayClipAtPoint(crashSound, Camera.main.transform.position, GetSoundVolumeNormalized());
                break;
        }
    }

    public void ChangeSoundValue()
    {
        soundVolume = (soundVolume+1) % SOUND_VOLUME_MAX;
        onSoundChangeEvent?.Invoke(this, EventArgs.Empty);
    }

    public int GetSoundVolume()
    {
        return soundVolume;
    }

    public float GetSoundVolumeNormalized()
    {
        return (float)soundVolume / SOUND_VOLUME_MAX;
    }

    private void Lander_OnOnPickupFuelEvent(object sender, EventArgs e)
    {
        AudioSource.PlayClipAtPoint(fuelPickUpSound, Camera.main.transform.position, GetSoundVolumeNormalized());
    }

    private void Lander_OnOnPickupCoinEvent(object sender, EventArgs e)
    {
        AudioSource.PlayClipAtPoint(coinPickUpSound, Camera.main.transform.position, GetSoundVolumeNormalized());
    }
}
