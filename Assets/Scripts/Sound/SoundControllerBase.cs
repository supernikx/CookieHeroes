using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public abstract class SoundControllerBase : MonoBehaviour
{
    [Header("General Sound Settings")]
    [SerializeField]
    protected SoundOutput output;
    [SerializeField]
    protected List<AudioSource> sources = new List<AudioSource>();

    GameManager gm;
    SoundManager soundMng;

    public virtual void Init(SoundManager _soundMng)
    {
        soundMng = _soundMng;
        soundMng.OnSoundUpdated += HandleOnSoundUpdated;

        AudioMixerGroup mixerGroup = soundMng.GetOutputGroup(output);
        foreach (AudioSource source in sources)
        {
            source.outputAudioMixerGroup = mixerGroup;
        }
    }

    protected virtual void HandleOnSoundUpdated(bool _toggle)
    {
        return;
    }

    protected AudioSource PlayAudioClip(AudioClipStruct _audioClipStruct)
    {
        if (!soundMng.IsSoundOn())
            return null;

        if (!_audioClipStruct.canUseMultipleSource)
        {
            foreach (AudioSource source in sources)
            {
                if (source.isPlaying && source.clip == _audioClipStruct.clip)
                    return null;
            }
        }

        foreach (AudioSource source in sources)
        {
            if (!source.isPlaying)
            {
                source.clip = _audioClipStruct.clip;
                source.loop = _audioClipStruct.loopable;
                source.Play();
                return source;
            }
        }

        return null;
    }

    protected void StopAudioClips()
    {
        foreach (AudioSource source in sources)
        {
            source.Stop();
        }
    }

    private void OnDisable()
    {
        if (soundMng != null)
            soundMng.OnSoundUpdated -= HandleOnSoundUpdated;
    }
}

[System.Serializable]
public struct AudioClipStruct
{
    public AudioClip clip;
    public bool canUseMultipleSource;
    public bool loopable;
}

public enum SoundOutput
{
    Music,
    SFX,
}
