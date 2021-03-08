using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class SoundEffectData
{

    public string parameter;
    public float from = 0;
    public float to = 0.2f;
    public float duration = 0.25f;

    public SoundEffectData() { }

    public SoundEffectData(SoundEffectData other)
    {
        this.parameter = other.parameter;
        this.from = other.from;
        this.to = other.to;
        this.duration = other.duration;
    }
}