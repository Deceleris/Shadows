using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundEffectController : MonoBehaviour
{

    [SerializeField] AudioMixer audioMixer;

    static SoundEffectController _current;
    public static SoundEffectController current {
        get {
            if (_current == null) {
                _current = GameObject.FindObjectOfType<SoundEffectController>();
            }
            return _current;
        }
    }

    int volumePriority;

    IEnumerator volumeRoutine;

    public static void PlayEffect(SoundEffectData effect, AnimationCurve curve, int direction)
    {
        AnimateParameter(effect.parameter, effect.from, effect.to, effect.duration, curve, direction);
    }

    public static void AnimateParameter(string parameter, float from, float to, float duration, AnimationCurve curve, int direction)
    {
        if (current.volumeRoutine != null) current.StopCoroutine(current.volumeRoutine);
        current.volumeRoutine = current.AnimationRoutine(from, to, duration, curve, direction,
            (float value) => current.audioMixer.SetFloat(parameter, value),
            () => {  }
        );
        Functions.StartCoroutine(current.volumeRoutine);
    }

    IEnumerator AnimationRoutine(float from, float to, float duration, AnimationCurve curve, int direction, System.Action<float> SetValue, System.Action onEnd)
    {
        float progress = 0;
        while (progress < 1) {
            progress += Time.deltaTime / duration;
            SetValue(Mathf.Lerp(from, to, direction == 1 ? curve.Evaluate(progress) : 1 - curve.Evaluate(progress)));
            yield return null;
        }
        onEnd?.Invoke();
    }
}