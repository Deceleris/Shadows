using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeCanvasGroup : MonoBehaviour
{

    public CanvasGroup group;
    public float duration;

    // ----------------------------- PARTIE CONTROLE D'UI

    public void Fade(float alpha)
    {
        StartCoroutine(FadeRoutine(alpha));
    }

    public IEnumerator FadeRoutine(float alpha)
    {
        if (alpha == 1) { group.gameObject.SetActive(true); group.alpha = 0; }
        float percent = 0;
        float start = group.alpha;

        while (percent < 1) {
            percent += Time.deltaTime / duration;
            group.alpha = Mathf.Lerp(start, alpha, percent);
            yield return null;
        }

        group.alpha = alpha;
        if (alpha == 0) group.gameObject.SetActive(false);
    }
}