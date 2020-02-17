using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SettingsUI : Singleton<SettingsUI>
{
    [Header("Panel / Settings")]
    [SerializeField] TextMeshProUGUI txt_Settings_Header;
    [SerializeField] Button btn_Settings_Home;
    [SerializeField] Button btn_Settings_Login;
    [SerializeField] Button btn_Settings_Vibration_On;
    [SerializeField] Button btn_Settings_Vibration_Off;
    [SerializeField] Slider sld_Settings_Music_Volume;
    [SerializeField] Slider sld_Settings_SFX_Volume;


    private void OnEnable()
    {
        OnClickAddListener();
    }

    private void OnDisable()
    {

    }

    void OnClickAddListener()
    {
        //btn_Settings_Home.onClick.AddListener(ShowMenuPanel);
        //btn_Settings_Home.onClick.AddListener(ShowMenuPanel);
        //btn_Settings_Vibration_Off.onClick.AddListener(VibrationOff);
        //btn_Settings_Vibration_On.onClick.AddListener(VibrationOn);
        //sld_Settings_Music_Volume.onValueChanged.AddListener(SetMusicVolume);
        //sld_Settings_Music_Volume.onValueChanged.AddListener(SetSfxVolume);
    }


    private void VibrationOn() { }
    private void VibrationOff() { }

    private void SetMusicVolume(float musicVolume) { }
    private void SetSfxVolume(float sfxVolume) { }
}
