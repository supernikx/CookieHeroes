using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IC.UIBase;

public class UIMenu_MainMenu : UIControllerBase
{
    public Action StartGameButtonPressed;
    public Action<bool> OnSoundToggle;
    public Action<bool> OnVibrationToggle;

    [Header("Panel References")]
    [SerializeField]
    private Toggle vibrationToggle;
    [SerializeField]
    private Toggle soundToggle;

    public override void CustomSetup()
    {
        base.CustomSetup();

        int soundPref = PlayerPrefs.GetInt("Sound", 0);
        soundToggle.isOn = (soundPref == 0);

        int vibrationPref = PlayerPrefs.GetInt("Vibration", 0);
        vibrationToggle.isOn = (vibrationPref == 0);
    }

    public void StartGame()
    {
        if (!IsEnable())
            return;

        StartGameButtonPressed?.Invoke();
    }

    public void ToggleSound(bool _value)
    {
        if (!IsEnable())
            return;

        int soundPref = _value ? 0 : 1;
        PlayerPrefs.SetInt("Sound", soundPref);

        OnSoundToggle?.Invoke(_value);
    }

    public void ToggleVibration(bool _value)
    {
        if (!IsEnable())
            return;

        int vibrationPref = _value ? 0 : 1;
        PlayerPrefs.SetInt("Vibration", vibrationPref);

        OnVibrationToggle?.Invoke(_value);
    }
}
