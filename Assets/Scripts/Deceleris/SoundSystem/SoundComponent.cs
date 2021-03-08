using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class SoundComponent
{

    public AudioClip clip;
    public RFloat volume = new RFloat();
    public RFloat pitch = new RFloat();

    public SoundComponent () { }

    public SoundComponent(SoundComponent other)
    {
        this.clip = other.clip;
        this.volume = other.volume;
        this.pitch = other.pitch;
    }
}