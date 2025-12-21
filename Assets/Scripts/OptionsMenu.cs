using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections.Generic;

public class OptionsMenu : MonoBehaviour
{

    Resolution[] resolutions;
    public Dropdown resolutionDropdown;

    void Start()
    {
    // Done using partly by using video tutorial and partly by documentation
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
    }
    public AudioMixer audioMixer;

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.UnloadSceneAsync("Options");
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        Debug.Log("Volume set to: " + volume);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        Debug.Log("Resolution set to: " + resolution.width + "x" + resolution.height);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        Debug.Log("Quality set to index: " + qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        Debug.Log("Fullscreen set to: " + isFullscreen);
    }
}
