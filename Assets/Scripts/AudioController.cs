using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static float masterVolume = 1.0f;
    public static float currentVolume = 0.0f;

    public float volume = 0;

    private void Update()
    {
        SetVolume(volume);
        currentVolume = volume;
    }

    public static void SetVolume(float volume)
    {
        AudioListener.volume = volume * masterVolume;
    }
}
