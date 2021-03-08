using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterMusic : StarterEffect
{

    [Header ("MUSIC")]
    public Sound music;

    public override void Initialise()
    {
        music.Play();
        base.Initialise();
    }
}