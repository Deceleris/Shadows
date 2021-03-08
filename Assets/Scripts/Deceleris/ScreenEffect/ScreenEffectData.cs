using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class ScreenEffectData
{
    public EffectType effectType = default;

    public float from = 0;
    public float to = 0.2f;

    public int imageIndex;
    public Color colorFrom;
    public Color colorTo;

    public float duration = 0.25f;
    public int priority = 0;

    public enum EffectType
    {
        Lens,
        Aberration,
        Saturation,
        Bloom,
        Image,
        ImageGroup,
    }

    public ScreenEffectData() { }

    public ScreenEffectData(ScreenEffectData other)
    {
        this.effectType = other.effectType;
        this.from = other.from;
        this.to = other.to;
        this.duration = other.duration;
        this.priority = other.priority;
    }

#if UNITY_EDITOR
    public void Draw()
    {
        EditorExtensions.DrawInLayout(false, "box", CEditor.window, true, false, () => {
            effectType = (EffectType)EditorGUILayout.EnumPopup(effectType);
            if (effectType == EffectType.Aberration || effectType == EffectType.Bloom || effectType == EffectType.Saturation || effectType == EffectType.Lens) {
                from = from.Draw("From");
                to = to.Draw("To");
                duration = duration.Draw("Duration");
                priority = priority.Draw("Priority");
            } else {
                imageIndex = imageIndex.Draw("Image index");
                colorFrom = colorFrom.Draw("From");
                colorTo = colorTo.Draw("To");
                duration = duration.Draw("Duration");
            }
        });
    }
#endif
}