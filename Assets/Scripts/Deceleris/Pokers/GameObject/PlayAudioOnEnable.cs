using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnEnable : MonoBehaviour
{

    public Sound sound;

    public void OnEnable()
    {
        sound.Play();
    }

}