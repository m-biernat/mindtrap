using UnityEngine;
using UnityEngine.UI;

public class ChangeSettingsUI : MonoBehaviour
{
    public PlayerUI playerUI;

    [Space]
    public Slider mouseSensitivitySlider;
    public Slider volumeSlider;

    [Space]
    public Text mouseSensitivityValue;
    public Text volumeValue;

    void Start()
    {
        ReloadFormElements();
    }

    private void ReloadFormElements()
    {
        mouseSensitivitySlider.value = ClientSettings.mouseSensitivity;
        mouseSensitivityValue.text = ClientSettings.mouseSensitivity.ToString();

        volumeSlider.value = ClientSettings.volume;
        volumeValue.text = (ClientSettings.volume * 100f).ToString();
    }

    public void SaveChanges()
    {
        playerUI.audioSource.Play();
        ClientSettings.SaveChanges();
        
        playerUI.ToggleView();
    }

    public void ResetChanges()
    {
        playerUI.audioSource.Play();
        ClientSettings.Load();
        ReloadFormElements();
    }

    public void LoadDefaults()
    {
        playerUI.audioSource.Play();
        ClientSettings.LoadDefaults();
        ReloadFormElements();
    }

    public void SetMouseSensitivity(float mouseSensitivity)
    {
        ClientSettings.mouseSensitivity = Mathf.Floor(mouseSensitivity * 10f) / 10f;
        mouseSensitivityValue.text = ClientSettings.mouseSensitivity.ToString();
    }

    public void SetVolume(float volume)
    {
        ClientSettings.volume = Mathf.Floor(volume * 100f) / 100f;
        volumeValue.text = (ClientSettings.volume * 100f).ToString();

        AudioListener.volume = ClientSettings.volume;
    }
}