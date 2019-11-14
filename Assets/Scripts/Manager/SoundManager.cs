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
}
