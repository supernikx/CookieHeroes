﻿using System.Collections;
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

    public virtual void Init(GameManager _gm)
    {
        gm = _gm;
        soundMng = gm.GetSoundManager();

        AudioMixerGroup mixerGroup = soundMng.GetOutputGroup(output);
        foreach (AudioSource source in sources)
        {
            source.outputAudioMixerGroup = mixerGroup;
        }
    }

    protected void PlayAudioClip(AudioClipStruct _audioClipStruct)
    {
        if (!_audioClipStruct.CanUseMultipleSource)
        {
            foreach (AudioSource source in sources)
            {
                if (source.isPlaying && source.clip == _audioClipStruct.clip)
                    return;
            }
        }

        foreach (AudioSource source in sources)
        {
            if (!source.isPlaying)
            {
                source.clip = _audioClipStruct.clip;
                source.Play();
                return;
            }
        }
    }

    protected void StopAudioClips()
    {
        foreach (AudioSource source in sources)
        {
            source.Stop();
        }
    }
}

[System.Serializable]
public struct AudioClipStruct
{
    public AudioClip clip;
    public bool CanUseMultipleSource;
}

public enum SoundOutput
{
    Music,
    SFX,
}