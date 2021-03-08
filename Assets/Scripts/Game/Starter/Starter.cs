using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour
{

    List<StarterEffect> effects;

    public void StartStarter ()
    {
        Initialise();
    }

    public virtual void Initialise ()
    {
        effects = new List<StarterEffect>();
        for (int i = 0; i < transform.childCount; i++) {
            GameObject child = transform.GetChild(i).gameObject;
            StarterEffect effect = child.gameObject.GetComponent<StarterEffect>();
            if (effect != null) effects.Add(effect);
        }

        foreach (StarterEffect effect in effects) {
            effect.Initialise();
        }
    }
}