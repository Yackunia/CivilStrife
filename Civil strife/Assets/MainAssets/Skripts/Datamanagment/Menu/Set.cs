using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Set : MonoBehaviour
{
    Resolution[] res;

    [SerializeField] private AudioMixer materMixer;

    [SerializeField] private Dropdown resDropDown;

    private void Start()
    {
        LoadResolution();
    }

    private void LoadResolution()
    {
        res = Screen.resolutions;

        resDropDown.ClearOptions();

        int currentResIndex = 0;
        List<string> options = new List<string>();

        for (int i = 0; i < res.Length; i++)
        {
            string option = res[i].width + " x " + res[i].height;
            options.Add(option);

            if (res[i].width == Screen.currentResolution.width &&
                res[i].height == Screen.currentResolution.height) currentResIndex = i;
        }

        resDropDown.AddOptions(options);
        resDropDown.value = currentResIndex;
        resDropDown.RefreshShownValue();
    }

    public void SetResolution(int resIndex)
    {
        Resolution resolution = res[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        materMixer.SetFloat("masterVolume",volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        Debug.Log(QualitySettings.GetQualityLevel());
    }

    public void SetScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
        
}
