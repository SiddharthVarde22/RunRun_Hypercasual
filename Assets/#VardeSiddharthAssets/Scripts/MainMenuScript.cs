using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField]
    GameObject mainMenuPanelRef, settingsPanelRef;
    [SerializeField]
    Slider musicSlider, effectsSlider;

    float currentMusicVolume, currentEffectsVolume;

    AudioManager audioManagerRef;
    

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        mainMenuPanelRef.SetActive(true);
        settingsPanelRef.SetActive(false);
        audioManagerRef = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

        if(PlayerPrefs.HasKey("musicVolume"))
        {
            currentMusicVolume = PlayerPrefs.GetFloat("musicVolume");
            musicSlider.value = currentMusicVolume;
        }

        if(PlayerPrefs.HasKey("effectsVolume"))
        {
            currentEffectsVolume = PlayerPrefs.GetFloat("effectsVolume");
            effectsSlider.value = currentEffectsVolume;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayPressed()
    {
        SceneManager.LoadScene(1);
    }

    public void OnSettingsPressed()
    {
        mainMenuPanelRef.SetActive(false);
        settingsPanelRef.SetActive(true);
    }

    public void OnSettingsBackPressed()
    {
        mainMenuPanelRef.SetActive(true);
        settingsPanelRef.SetActive(false);
    }

    public void OnQuitPressed()
    {
        Application.Quit();
    }

    public void OnMusicVolumeChanged(float newVolume)
    {
        audioManagerRef.ChangeMusicVolume(newVolume);
        currentMusicVolume = newVolume;
        PlayerPrefs.SetFloat("musicVolume", currentMusicVolume);
    }

    public void OnEffectsVolumeChanged(float newVolume)
    {
        audioManagerRef.ChangeEfferctsVolume(newVolume);
        currentEffectsVolume = newVolume;
        PlayerPrefs.SetFloat("effectsVolume", currentEffectsVolume);
    }
}
