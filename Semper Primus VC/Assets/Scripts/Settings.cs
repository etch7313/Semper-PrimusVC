using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField]
    private GameObject MenuCanvas;
    [SerializeField]
    private GameObject SettingsCanvas;
    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject mainMenu;
 
    
        
    
    
    

   //--------------------------------------------------------------------------------
    public void GoToSettings()
    {
        LeanTween.scale(mainMenu, Vector3.zero, 0.25f);
        LeanTween.scale(settingsMenu, new Vector3(1, 1, 1f), 0.25f).setDelay(0.25f);
    }

    
    
    
    public void GoBackToMenu()
    {
        LeanTween.scale(settingsMenu, Vector3.zero, 0.25f);
        LeanTween.scale(mainMenu, new Vector3(1, 1, 1f), 0.25f).setDelay(0.25f);
    }

    
    
    public void setMasterVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void setMusicVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }




    public void SetQuality(int _qualityIndex)
    {
        QualitySettings.SetQualityLevel(_qualityIndex);
    }

    
}
