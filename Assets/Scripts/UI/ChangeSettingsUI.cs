using UnityEngine;
using UnityEngine.UI;

public class ChangeSettingsUI : MonoBehaviour
{
    public Slider mouseSensitivitySlider;
    public Slider volumeSlider;

    void Start()
    {
        ReloadFormElements();
    }

    private void ReloadFormElements()
    {
        if (mouseSensitivitySlider)
            mouseSensitivitySlider.value = ClientSettings.mouseSensitivity;

        if (volumeSlider)
            volumeSlider.value = ClientSettings.volume;
    }

    public void SaveChanges()
    {
        ClientSettings.SaveChanges();
    }

    public void ResetChanges()
    {
        ClientSettings.Load();
        ReloadFormElements();
    }

    public void LoadDefaults()
    {
        ClientSettings.LoadDefaults();
        ReloadFormElements();
    }

    public void SetMouseSensitivity(float mouseSensitivity)
    {
        ClientSettings.mouseSensitivity = Mathf.Floor(mouseSensitivity * 10f) / 10f;
    }

    public void SetVolume(float volume)
    {
        ClientSettings.volume = (byte)volume;
    }
}