using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
    [SerializeField]
    private GameObject highScoreText;
    [SerializeField]
    private TextMeshProUGUI higScoreScore;

    public override void CustomSetup()
    {
        base.CustomSetup();

        int soundPref = PlayerPrefs.GetInt("Sound", 0);
        soundToggle.isOn = (soundPref == 0);

        int vibrationPref = PlayerPrefs.GetInt("Vibration", 0);
        vibrationToggle.isOn = (vibrationPref == 0);
    }

    public override void ToggleMenu(bool _value)
    {
        base.ToggleMenu(_value);

        if (_value)
        {
            int highScore = PlayerPrefs.GetInt("HighScore", 0);
            if (highScore > 0)
            {
                higScoreScore.text = highScore.ToString();
                highScoreText.SetActive(true);
            }
            else
                highScoreText.SetActive(false);
        }
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
