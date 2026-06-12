using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static float musicTime;
    private AudioSource musicAudioSource;

    void Awake()
    {
        musicAudioSource = GetComponent<AudioSource>();
        musicAudioSource.time = musicTime;
    }



    void Update()
    {
        musicTime = musicAudioSource.time;
    }
}
