using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSoundController : SoundControllerBase
{
    [Header("Music Settings")]
    [SerializeField]
    private AudioClipStruct mainMenuAudioClip;
    [SerializeField]
    private AudioClipStruct gameAudioClip;
    [SerializeField]
    private AudioClipStruct endGameAudioClip;

    [Header("Additivie Game Music Settings")]
    [SerializeField]
    private AudioClipStruct additiveGameAudioClip;
    [SerializeField]
    private float additiveGameAudioClipTime;

    private IEnumerator gameDelayClipRoutine;

    public void PlayMainMenuClip()
    {
        StopAudioClips();
        if (gameDelayClipRoutine != null)
            StopCoroutine(gameDelayClipRoutine);

        PlayAudioClip(mainMenuAudioClip);
    }

    public void PlayGameClip()
    {
        StopAudioClips();
        if (gameDelayClipRoutine != null)
            StopCoroutine(gameDelayClipRoutine);

        AudioSource clipSource = PlayAudioClip(gameAudioClip);
        gameDelayClipRoutine = GameDelayClipCoroutine(clipSource);
        StartCoroutine(gameDelayClipRoutine);
    }

    public void PlayEndGameClip()
    {
        StopAudioClips();
        if (gameDelayClipRoutine != null)
            StopCoroutine(gameDelayClipRoutine);
        PlayAudioClip(endGameAudioClip);
    }

    public void StopClip()
    {
        StopAudioClips();
    }

    private IEnumerator GameDelayClipCoroutine(AudioSource _clipSource)
    {
        bool oneTime = false;
        while (_clipSource.clip == gameAudioClip.clip)
        {
            if (_clipSource.time >= additiveGameAudioClipTime && !oneTime)
            {
                oneTime = true;
                PlayAudioClip(additiveGameAudioClip);
            }

            if (_clipSource.time < additiveGameAudioClipTime)
                oneTime = false;

            yield return null;
        }
    }
}
