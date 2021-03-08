using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    // ==================================================== VARIABLES

    [Header ("POOLING")]
    public List<SoundSource> desactivated = new List<SoundSource>();
    public List<SoundSource> activated = new List<SoundSource>();
    public List<SoundSource> instances = new List<SoundSource>();

    // ==================================================== TOOLS

    public SoundSource SortInstance (Sound sound)
    {
        SoundSource instance;
        VerifyLists();

        // If there is no desactivated instance, we create one
        if (desactivated.Count == 0) {
            instance = new GameObject("Sound Instance").AddComponent<SoundSource>();
            instance.transform.SetParent(transform);
        // Else, we get the oldest desactivated source 
        } else {
            instance = desactivated[0];
            desactivated.RemoveAt(0);
        }

        instance.gameObject.SetActive(true);
        instance.LinkTo(sound);
        instance.gameObject.name = "► " + sound.name;

        activated.Add(instance);
        instances.Add(instance);

        return instance;
    }

    public void StopAll ()
    {
        List<SoundSource> temps = new List<SoundSource>(instances);
        foreach (SoundSource instance in temps) {
            if (instance == null) continue;
            instance.Stop();
        }
        VerifyLists();
    }

    public void DestroyAll ()
    {
        List<SoundSource> temps = new List<SoundSource>(instances);
        foreach (SoundSource instance in temps) {
            if (instance == null) continue;
            instance.Stop();
            DestroyImmediate(instance.gameObject);
        }
        VerifyLists();
    }

    public void VerifyLists ()
    {
        List<SoundSource> temp = new List<SoundSource>();
        foreach (SoundSource instance in desactivated) { if (instance != null) temp.Add(instance); }
        desactivated = temp;

        temp = new List<SoundSource>();
        foreach (SoundSource instance in activated) { if (instance != null) temp.Add(instance); }
        activated = temp;

        temp = new List<SoundSource>();
        foreach (SoundSource instance in instances) { if (instance != null) temp.Add(instance); }
        instances = temp;
    }

    // ======================================================= Singleton pattern

    static SoundManager _current;
    public static SoundManager current { get {
            if(_current == null) 
                _current = FindObjectOfType<SoundManager>();
            if (_current == null)
                _current = new GameObject("Sound Manager").AddComponent<SoundManager>();
            return _current;
        } 
    }
}