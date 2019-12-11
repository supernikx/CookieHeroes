using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSoundController : SoundControllerBase
{
    [Header("General Settings")]
    [SerializeField]
    private AudioClipStruct clip;

    public void PlayClip(bool _overrideClip = false)
    {
        PlayAudioClip(clip, _overrideClip);
    }

    public void StopClip()
    {
        StopAudioClips();
    }

    protected override void HandleOnSoundUpdated(bool _toggle)
    {
        if (_toggle)
            PlayClip();
        else
            StopClip();
    }
}
