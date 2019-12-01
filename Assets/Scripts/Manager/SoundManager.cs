using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    [Header("Sound Settings")]
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private AudioMixerGroup musicOutput;
    [SerializeField]
    private AudioMixerGroup sfxOutput;

    private GameManager gm;
    UIMenu_MainMenu mainMenuPanel;
    private bool soundOn;

    public void Setup(GameManager _gm)
    {
        gm = _gm;
        mainMenuPanel = gm.GetUIManager().GetMenu<UIMenu_MainMenu>();

        SoundControllersSetup();

        mainMenuPanel.OnSoundToggle += HandleOnSoundToggle;
        soundOn = PlayerPrefs.GetInt("Sound", 0) == 0;
    }

    public AudioMixerGroup GetOutputGroup(SoundOutput _output)
    {
        switch (_output)
        {
            case SoundOutput.Music:
                return musicOutput;
            case SoundOutput.SFX:
                return sfxOutput;
        }

        return null;
    }

    public bool IsSoundOn()
    {
        return soundOn;
    }

    private void SoundControllersSetup()
    {
        SoundControllerBase[] soundCtrls = FindObjectsOfType<SoundControllerBase>();
        for (int i = 0; i < soundCtrls.Length; i++)
        {
            soundCtrls[i].Init(this);
        }
    }

    private void HandleOnSoundToggle(bool _value)
    {
        soundOn = _value;
    }

    private void OnDisable()
    {
        mainMenuPanel.OnSoundToggle -= HandleOnSoundToggle;
    }
}
