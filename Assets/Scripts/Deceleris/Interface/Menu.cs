using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Cette classe permet de contrôler un menu elle est virtuelle et tout les menu en hériteront
// Elle aura le contrôle de la navigation, ouverture / fermeture et callback lorsque fermeture

public class Menu : MonoBehaviour
{

    // ============================================================= VARIABLES

    [Header("REFERENCES")]
    [SerializeField] CanvasGroup graphics;
    [SerializeField] GameObject firstElementToSelect;

    // ********* BOOKMARKERS
    bool isOpen;
    Menu previousMenu;
    GameObject selectedObject;

    IEnumerator fadingRoutine;

    // ============================================================= LOCAL



    // ============================================================= CORPS

    // --------- Lorsque l'information vient du menu lui même (boutons)

    public virtual void TryOpen ()
    {
        MenuManager.current.OpenMenu(this);
    }

    public virtual void TryClose ()
    {
        MenuManager.current.CloseMenu(this);
    }

    // --------- Lorsque le manager indique / valide l'action

    public virtual void SetOpen (Menu currentMenu, bool replacePrevious, bool fade = true)
    {
        if (fade && graphics != null) Fade(0, 1);
        this.previousMenu = replacePrevious ? currentMenu : this.previousMenu == null ? currentMenu : previousMenu;

        EventSystem.current.SetSelectedGameObject(selectedObject == null ? firstElementToSelect : selectedObject);
        selectedObject = EventSystem.current.currentSelectedGameObject;
        isOpen = true;
    }

    public virtual void SetClosed (bool fade = true)
    {
        if (fade && graphics != null) Fade(1, 0);
        previousMenu.TryOpen();

        selectedObject = EventSystem.current.currentSelectedGameObject;
        isOpen = false;
    }

    public virtual void InstantClose ()
    {
        if (graphics != null) {
            graphics.alpha = 0;
            graphics.gameObject.SetActive(false);
        }
    }

    // --------- Visuel

    public virtual void Fade (float from, float to)
    {
        graphics.gameObject.SetActive(true);

        if (fadingRoutine != null) StopCoroutine(fadingRoutine);
        fadingRoutine = FadingRoutine();
        StartCoroutine(fadingRoutine);

        IEnumerator FadingRoutine ()
        {

            float progress = 0;
            while (progress < 1) {
                progress += 1f / SettingsSet.current.fadingDuration;
                graphics.alpha = Mathf.Lerp(from, to, SettingsSet.current.fadingCurve.Evaluate(progress));
                yield return null;
            }

            graphics.alpha = to;
            graphics.gameObject.SetActive(to > 0);
        }
    }

}