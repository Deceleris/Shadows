using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PlayAudioOnEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    public Sound enterSound;
    public Sound clickSound;
    public Sound exitSound;

    Selectable selectable;

    public void Awake()
    {
        selectable = GetComponent<Selectable>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (selectable != null && !selectable.interactable) return;
        if (clickSound != null) clickSound.Play();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (selectable != null && !selectable.interactable) return;
        if (enterSound != null) enterSound.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (selectable != null && !selectable.interactable) return;
        if (exitSound != null) exitSound.Play();
    }
}