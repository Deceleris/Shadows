using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterClock : StarterEffect
{

    public float rotationSpeed = 1.8625f;
    public float launchTime = 3.75f;
    public List<MovementRotation> clockwiseds = new List<MovementRotation>();
    public List<MovementRotation> counterCloks = new List<MovementRotation>();
    public List<ResetRotation> reset = new List<ResetRotation> ();

    bool started = false;

    public override void Initialise()
    {
        foreach (ResetRotation rotation in reset) rotation.Reset();
        foreach (MovementRotation clockWised in clockwiseds) { 
            clockWised.enabled = false;
            clockWised.speed = rotationSpeed;
        }
        foreach (MovementRotation counterClok in counterCloks) { 
            counterClok.enabled = false;
            counterClok.speed = -rotationSpeed;
        }

        started = false;

        base.Initialise();
    }

    public override void Update()
    {
        if (time > launchTime && !started) {
            started = true;
            Launch();
        }

        base.Update();
    }

    public void Launch ()
    {
        foreach (ResetRotation rotation in reset) rotation.Reset();
        foreach (MovementRotation clockWised in clockwiseds) { clockWised.enabled = true; }
        foreach (MovementRotation counterClok in counterCloks) { counterClok.enabled = true; }
    }

    public void Stop ()
    {
        foreach (ResetRotation rotation in reset) rotation.Reset();
        foreach (MovementRotation clockWised in clockwiseds) { clockWised.enabled = false; }
        foreach (MovementRotation counterClok in counterCloks) { counterClok.enabled = false; }
    }

}