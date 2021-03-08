using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class have two fonctionnalities :
// - It's called by a sound for every clips
// - He follow the volume variation of a sound, so you can also assign it if you have an audiosource in your game

[RequireComponent (typeof(AudioSource))]
public class SoundSource : MonoBehaviour
{

    public Sound sound;
    public SoundComponent soundClip;

    // This is not public, because we NEED an audio source
    AudioSource player;

    // This is started when the instance is launched
    IEnumerator stopTimer;

    // Instance values can be random
    float startVolume;
    float startPitch;

    // ========================== INIT

    public void LinkTo (Sound sound)
    {
        this.sound = sound;
        sound.sources.Add(this);

        if (player == null) player = GetComponent<AudioSource>();
    }

    public void Play (SoundComponent soundClip)
    {
        this.soundClip = soundClip;

        player.clip = soundClip.clip;
        player.outputAudioMixerGroup = sound.audioMixer;

        startVolume = soundClip.volume.sort;
        player.volume = sound.volume * startVolume;

        startPitch = soundClip.pitch.sort;
        player.pitch = sound.pitch * startPitch;

        player.Play();

        if (stopTimer != null) StopCoroutine(stopTimer);
        stopTimer = StopTimer();
        StartCoroutine(StopTimer()); 
    }

    public IEnumerator StopTimer()
    {
        yield return new WaitForSeconds(soundClip.clip.length);
        Stop();
    }

    public void Stop ()
    {
        SoundManager sm = SoundManager.current;

        if (stopTimer != null) StopCoroutine(stopTimer);

        gameObject.name = "Desactivated";
        gameObject.SetActive(false);
        sound.sources.Remove(this);
        sm.activated.Remove(this);
        sm.desactivated.Add(this);
    }
}