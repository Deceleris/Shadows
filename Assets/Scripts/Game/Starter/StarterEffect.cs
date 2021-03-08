using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterEffect : MonoBehaviour
{

    public float startTime;
    public float time;

    public virtual void Initialise ()
    {
        startTime = Time.time;
    }

    public virtual void Update ()
    {
        time += Time.deltaTime;
    }
}