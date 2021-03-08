using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ScreenEffectController : MonoBehaviour
{

    [SerializeField] VolumeProfile volumeProfile;
    [SerializeField] List<Image> images = new List<Image>();
    [SerializeField] List<ImageGroup> imageGroups = new List<ImageGroup>();

    [System.Serializable]
    public class ImageGroup
    {
        public List<Image> images = new List<Image>();
    }

    LensDistortion lensDistortion;
    ChromaticAberration aberration;
    ColorAdjustments saturation;
    Bloom bloom;

    int lensPriority;
    int aberrationPriority;
    int saturationPriority;
    int bloomPriority;

    IEnumerator lensRoutine;
    IEnumerator aberrationRoutine;
    IEnumerator saturationRoutine;
    IEnumerator bloomRoutine;

    static ScreenEffectController _current;
    public static ScreenEffectController current { get {
            if (_current == null) {
                _current = GameObject.FindObjectOfType<ScreenEffectController>();
                _current.Initialise();
            }
            return _current;
        }
    }

    void Awake ()
    {
        Initialise();
    }

    private void Initialise()
    {
        volumeProfile.TryGet(out lensDistortion);
        volumeProfile.TryGet(out aberration);
        volumeProfile.TryGet(out saturation);
        volumeProfile.TryGet(out bloom);
    }

    public static void PlayEffect (ScreenEffectData effect, AnimationCurve curve)
    {
        switch (effect.effectType) {
            case ScreenEffectData.EffectType.Lens:
            AnimateLens(effect.from, effect.to, effect.duration, effect.priority, curve);
            break;
            case ScreenEffectData.EffectType.Aberration:
            AnimateAberration(effect.from, effect.to, effect.duration, effect.priority, curve);
            break;
            case ScreenEffectData.EffectType.Saturation:
            AnimateSaturation(effect.from, effect.to, effect.duration, effect.priority, curve);
            break;
            case ScreenEffectData.EffectType.Bloom:
            AnimateBloom(effect.from, effect.to, effect.duration, effect.priority, curve);
            break;
            case ScreenEffectData.EffectType.Image:
            AnimateImage(effect.imageIndex, effect.colorFrom, effect.colorTo, effect.duration, curve);
            break;
            case ScreenEffectData.EffectType.ImageGroup:
            AnimateImageGroup(effect.imageIndex, effect.colorFrom, effect.colorTo, effect.duration, curve);
            break;
        }
    }

    public static void AnimateLens (float from, float to, float duration, int priority, AnimationCurve curve)
    {
        if (current.lensPriority > priority) return;
        current.lensPriority = priority;
        if (current.lensRoutine != null) current.StopCoroutine(current.lensRoutine);
        current.lensRoutine = current.AnimationRoutine(from, to, duration, curve, 
            (float value) => current.lensDistortion.intensity.value = value,
            () => { current.lensPriority = 0; current.lensDistortion.intensity.value = from; }
        );
        Functions.StartCoroutine(current.lensRoutine);
    }

    public static void AnimateAberration(float from, float to, float duration, int priority, AnimationCurve curve)
    {
        if (current.aberrationPriority > priority) return;
        current.aberrationPriority = priority;
        if (current.aberrationRoutine != null) current.StopCoroutine(current.aberrationRoutine);
        current.aberrationRoutine = current.AnimationRoutine(from, to, duration, curve,
            (float value) => current.aberration.intensity.value = value,
            () => { current.aberrationPriority = 0; current.aberration.intensity.value = from; }
        );
        Functions.StartCoroutine(current.aberrationRoutine);
    }

    public static void AnimateSaturation(float from, float to, float duration, int priority, AnimationCurve curve)
    {
        if (current.saturationPriority > priority) return;
        current.saturationPriority = priority;
        if (current.saturationRoutine != null) current.StopCoroutine(current.saturationRoutine);
        current.saturationRoutine = current.AnimationRoutine(from, to, duration, curve,
            (float value) => current.saturation.saturation.value = value,
            () => { current.saturationPriority = 0; current.saturation.saturation.value = from; }
        );
        Functions.StartCoroutine(current.saturationRoutine);
    }

    public static void AnimateBloom(float from, float to, float duration, int priority, AnimationCurve curve)
    {
        if (current.bloomPriority > priority) return;
        current.bloomPriority = priority;
        if (current.bloomRoutine != null) current.StopCoroutine(current.bloomRoutine);
        current.bloomRoutine = current.AnimationRoutine(from, to, duration, curve,
            (float value) => current.bloom.intensity.value = value,
            () => { current.bloomPriority = 0; current.bloom.intensity.value = from; }
        );
        Functions.StartCoroutine(current.bloomRoutine);
    }

    public static void AnimateImage (int index, Color from, Color to, float duration, AnimationCurve curve)
    {
        IEnumerator routine = current.AnimationRoutine(0, 1, duration, curve,
            (float value) => { current.images[index].color = Color.Lerp(from, to, value); },
            () => { }
        );
        Functions.StartCoroutine(routine);
    }

    public static void AnimateImageGroup(int index, Color from, Color to, float duration, AnimationCurve curve)
    {
        IEnumerator routine = current.AnimationRoutine(0, 1, duration, curve,
            (float value) => {
                List<Image> imagesTarget = current.imageGroups[index].images;
                foreach (Image image in imagesTarget)
                    image.color = Color.Lerp(from, to, value); 
            },
            () => { }
        );
        Functions.StartCoroutine(routine);
    }

    IEnumerator AnimationRoutine(float from, float to, float duration, AnimationCurve curve, System.Action<float> SetValue, System.Action onEnd)
    {
        float progress = 0;
        while (progress < 1) {
            progress += Time.deltaTime / duration;
            SetValue(Mathf.Lerp(from, to, curve.Evaluate(progress)));
            yield return null;
        }
        onEnd();
    }
}