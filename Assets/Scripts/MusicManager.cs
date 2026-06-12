using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static int musicVolume = 4;
    private const int MAX_MUSIC_VOLUME = 10;
    public static MusicManager Instance { get; private set;  }
        
    private static float musicTime;
    private AudioSource musicAudioSource;

    void Awake()
    {
        Instance = this;
        musicAudioSource = GetComponent<AudioSource>();
        musicAudioSource.time = musicTime;
    }

    void Start()
    {
        musicAudioSource.volume = GetMusicVolumeNormalized();
    }

    void Update()
    {
        musicTime = musicAudioSource.time;
    }

    public void ChangeMusicVolume()
    {
        musicVolume = (musicVolume + 1) % MAX_MUSIC_VOLUME;
        musicAudioSource.volume = GetMusicVolumeNormalized();
    }

    public int GetMusicVolume()
    {
        return musicVolume;
    }

    public float GetMusicVolumeNormalized()
    {
        return (float)musicVolume / MAX_MUSIC_VOLUME;
    }
}
