using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Functions : MonoBehaviour
{

    // ------------------------------------------------- PARTIE CREATION

    static Functions _current;
    static Functions current { get { if (_current == null) CreateFunctions(); return _current; } }

    static void CreateFunctions ()
    {
        _current = FindObjectOfType<Functions>();
        if (_current == null)
        {
            print("le porteur de fonction à dû être crée");
            _current = new GameObject("Functions").AddComponent<Functions>();
        }
    }

    // ------------------------------------------------- PARTIE UTILITAIRE

    public static new Coroutine StartCoroutine(IEnumerator routine)
    {
        return ((MonoBehaviour)current).StartCoroutine(routine);
    }

    public static new void StopCoroutine (IEnumerator routine)
    {
        ((MonoBehaviour)current).StopCoroutine(routine);
    }

    static IEnumerator WaitAndCall(float time, System.Action func)
    {
        yield return new WaitForSeconds(time);
        func();
    }

    public static void CallAfter(float time, System.Action func)
    {
        if (time == 0)
            func();
        else
            StartCoroutine(WaitAndCall(time, func));
    }

    public static void CallMultipleTimes(int times, System.Action func, float delay = 0)
    {
        for (int i = 0; i < times; i++)
            CallAfter(i * delay, func);
    }

}