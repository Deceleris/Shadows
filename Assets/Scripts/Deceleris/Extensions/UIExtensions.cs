using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class UIExtensions
{

    public static void PopUpAsTransition (this Image image, Color color, float fadeTime, AnimationCurve curve, bool fadeIn, bool fadeOut, System.Action transition = null) {
        Functions.StartCoroutine (Routine ());
        IEnumerator Routine () {
            if (fadeIn) yield return FadeImage (image, Color.clear, color, fadeTime, curve);
            transition?.Invoke ();
            if (fadeOut) yield return FadeImage (image, color, Color.clear, fadeTime, curve);
        }
    }

    public static void PopUpImage (this Image image, float fadeTime, float showTime, AnimationCurve curve, System.Action onEnd = null) {
        Functions.StartCoroutine (Routine ());
        IEnumerator Routine () {
            yield return FadeImage (image, Color.clear, Color.white, fadeTime, curve);
            yield return new WaitForSeconds (showTime);
            yield return FadeImage (image, Color.white, Color.clear, fadeTime, curve);
            onEnd?.Invoke ();
        }
    }

    public static void PopUpImage (this Image image, float fadeTime, float showTime, System.Action onEnd = null) {
        Functions.StartCoroutine (Routine ());
        IEnumerator Routine () {
            yield return FadeImage (image, Color.clear, Color.white, fadeTime);
            yield return new WaitForSeconds (showTime);
            yield return FadeImage (image, Color.white, Color.clear, fadeTime);
            onEnd?.Invoke ();
        }
    }

    public static IEnumerator FadeImage(this Image image, Color a, Color b, float time, AnimationCurve curve = null)
    {
        yield return Functions.StartCoroutine(image.FadeImageRoutine(a, b, time, curve));
    }

    public static IEnumerator FadeImageRoutine (this Image image, Color a, Color b, float time, AnimationCurve curve = null)
    {
        float percent = 0;
        float interpolation = 0;
        while (percent < 1) {
            percent += Time.deltaTime / time;
            interpolation = curve == null ? percent : curve.Evaluate (percent);
            image.color = Color.Lerp(a, b, interpolation);
            yield return null;
        }
        image.color = b;
    }

}