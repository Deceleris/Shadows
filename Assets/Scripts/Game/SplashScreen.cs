using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour
{

    // ============================================================= VARIABLES

    [Header("ELEMENTS")]
    [SerializeField] Image logo;
    [SerializeField] List<Text> letters = new List<Text>();
    [SerializeField] Image circle;

    [Header("SEQUENCES")]
    [SerializeField] float startDelay = 0.5f;
    [SerializeField] float logoDuration = 0.5f;
    [SerializeField] float letterDuration = 0.1f;
    [SerializeField] float fillDuration = 1f;
    [SerializeField] float bumpDuration = 1f;

    [Header("PARAMETERS")]
    [SerializeField] float logoSlideDistance = 25;
    [SerializeField] float letterUpDistance = 10;
    [SerializeField] float letterDelay = 0.1f;
    [SerializeField] float bumpFactor = 1.5f;
    [SerializeField] float rotationAmount = 180;

    [Header("CURVES")]
    [SerializeField] AnimationCurve fadeCurve;
    [SerializeField] AnimationCurve slideCurve;
    [SerializeField] AnimationCurve upCurve;
    [SerializeField] AnimationCurve fillCurve;
    [SerializeField] AnimationCurve rotationCurve;
    [SerializeField] AnimationCurve bumpCurve;

    float speed;

    // ============================================================= LOCAL

    void Start ()
    {
        Initialise();
        StartCoroutine(Animation());
    }

    // ============================================================= CORPS

    void Initialise ()
    {
        // Réinitialisation des éléments 
        logo.color = Color.white.WithA (0);
        logo.rectTransform.anchoredPosition = Vector2.one * logoSlideDistance;
        for (int i = 0; i < letters.Count; i++) letters[i].color = letters[i].color.WithA(0);
        circle.fillAmount = 0;
        circle.transform.localScale = Vector3.one;

        // On détermine le facteur de viutesse, en fonction de 
        // La durée totale et de la durée désirée
        float currentDuration =startDelay + logoDuration * 2 +letterDelay * letters.Count +fillDuration + bumpDuration;
        float desiredDuration = SettingsSet.current.splashDuration;
        speed = currentDuration / desiredDuration;
    }
    
    IEnumerator Animation ()
    {
        // Début
        yield return new WaitForSeconds(startDelay);
        yield return StartCoroutine(AnimateLogo(0, 1));

        // Lettre
        for (int i = 0; i < letters.Count; i++) {
            yield return new WaitForSeconds(letterDelay);
            StartCoroutine(UpLetter(letters[i]));
        }

        // Cercle
        yield return StartCoroutine(FillCircle());

        // Fin
        StartCoroutine(BumpCircle());
        for (int i = 0; i < letters.Count; i++) letters[i].color = letters[i].color.WithA(0);
        yield return StartCoroutine(AnimateLogo(1, 0));
        AppManager.LoadMainMenu(true);
    }

    IEnumerator AnimateLogo(float from, float to)
    {
        float percent = 0;
        while (percent < 1) {
            percent += Time.deltaTime / logoDuration * speed;
            logo.color = Color.white.WithA(Mathf.Lerp (from, to, fadeCurve.Evaluate(percent)));
            logo.rectTransform.anchoredPosition = Vector2.one * Mathf.Lerp (from, to, slideCurve.Evaluate(percent));
            yield return null;
        }
    }

    IEnumerator UpLetter (Text letter)
    {
        float percent = 0;
        float startY = letter.rectTransform.anchoredPosition.y;
        float startX = letter.rectTransform.anchoredPosition.x;
        while (percent < 1) {
            percent += Time.deltaTime / letterDuration * speed;
            letter.color = letter.color.WithA(Mathf.Lerp (0, 1, fadeCurve.Evaluate(percent)));
            letter.rectTransform.anchoredPosition = new Vector2(startX, startY + Mathf.Lerp(0, letterUpDistance, upCurve.Evaluate(percent)));
            yield return null;
        }
    }

    IEnumerator FillCircle ()
    {
        float percent = 0;
        while (percent < 1) {
            percent += Time.deltaTime / fillDuration * speed;
            circle.fillAmount = fillCurve.Evaluate(percent);
            circle.transform.localEulerAngles = -Vector3.forward * rotationCurve.Evaluate(percent) * rotationAmount;
            yield return null;
        }
    }

    IEnumerator BumpCircle ()
    {
        float percent = 0;
        while (percent < 1) {
            percent += Time.deltaTime / bumpDuration * speed;
            circle.transform.localScale = Vector3.one * bumpCurve.Evaluate(percent) * bumpFactor;
            circle.color = circle.color.WithA(Mathf.Lerp (1, 0, fadeCurve.Evaluate(percent)));
            yield return null;
        }
    }

}