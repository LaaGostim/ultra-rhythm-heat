using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenuManager : MonoBehaviour
{
    Resolution[] resolutions;
    public Dropdown resolutionDropdown;
    public AudioMixer musicMixer;
    public AudioMixer soundsMixer;
    public Slider mVolumeSlider;
    public Slider sVolumeSlider;

    void Start()
    {
        musicMixer.SetFloat("volumeM", PlayerPrefs.GetFloat("MusicVolume", 1f));
        mVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);

        soundsMixer.SetFloat("volumeS", PlayerPrefs.GetFloat("SoundVolume", 1f));
        sVolumeSlider.value = PlayerPrefs.GetFloat("SoundVolume", 1f);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + " @" + resolutions[i].refreshRate + "hz";
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    void Update()
    {

    }

    public void SetVolumeMusic(float volumeM)
    {
        musicMixer.SetFloat("volumeM", volumeM);
        PlayerPrefs.SetFloat("MusicVolume", volumeM);
    }
    public void SetVolumeSounds(float volumeS)
    {
        soundsMixer.SetFloat("volumeS", volumeS);
        PlayerPrefs.SetFloat("SoundVolume", volumeS);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}

