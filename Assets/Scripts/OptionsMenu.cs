using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class OptionsMenu : MonoBehaviour    // Manages the options menu for audio and display settings
{

    Resolution[] resolutions;
    public Dropdown resolutionDropdown;
    public static bool OpenedFromGame = false;
    public AudioMixer audioMixer;
    void Start()    // Initialize resolution options and load saved settings
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width &&
            resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // Load saved volumes
        if (PlayerPrefs.HasKey("MusicVolume"))
        // Load music volume
        {
            float musicValue = PlayerPrefs.GetFloat("MusicVolume");
            audioMixer.SetFloat("MusicVolume", musicValue);
            GameObject.Find("Volume(1)").GetComponent<Slider>().value = musicValue;
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        // Load SFX volume
        {
            float sfxValue = PlayerPrefs.GetFloat("SFXVolume");
            audioMixer.SetFloat("SFXVolume", sfxValue);
            GameObject.Find("Volume(2)").GetComponent<Slider>().value = sfxValue;
        }

        if (PlayerPrefs.HasKey("volume"))
        // Load master volume
        {
            float masterValue = PlayerPrefs.GetFloat("volume");
            audioMixer.SetFloat("volume", masterValue);
            GameObject.Find("Volume").GetComponent<Slider>().value = masterValue;
        }
    }

    public void BackToMainMenu()    // Close options menu and return to previous scene
    {
        Time.timeScale = 1f;
        if (OpenedFromGame)
        {
            PlayerMovementPlatformer.optionsOpen = false;
            OpenedFromGame = false;
            SceneManager.UnloadSceneAsync("Options");
        }
        else
        {
            SceneManager.UnloadSceneAsync("Options");
        }
    }

    public void SetVolume(float sliderValue)    // Set master volume
    {
        audioMixer.SetFloat("volume", sliderValue);
        PlayerPrefs.SetFloat("volume", sliderValue);
        Debug.Log("Volume set to: " + sliderValue + " dB");
    }

    public void SetMusicVolume(float sliderValue)   // Set music volume
    {
        audioMixer.SetFloat("MusicVolume", sliderValue);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }

    public void SetSFXVolume(float sliderValue)  // Set SFX volume
    {
        audioMixer.SetFloat("SFXVolume", sliderValue);
        PlayerPrefs.SetFloat("SFXVolume", sliderValue);
    }

    public void SetResolution(int resolutionIndex)  // Set screen resolution
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        Debug.Log("Resolution set to: " + resolution.width + "x" + resolution.height);
    }

    public void SetFullscreen(bool isFullscreen)    // Set fullscreen mode
    {
        Screen.fullScreen = isFullscreen;
        Debug.Log("Fullscreen set to: " + isFullscreen);
    }
    public void QuitGame()   // Quit the game application
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
