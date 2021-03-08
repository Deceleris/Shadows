using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
[CreateAssetMenu (menuName = "Content/Effect/ScreenEffect", fileName = "ScreenEffect")]
public class ScreenEffect : ScriptableObject
{
    // ==================================== VARIABLES

    public AnimationCurve curve = default;
    public List<ScreenEffectData> effects = new List<ScreenEffectData>();

    (int, int) test;

    public void Play ()
    {
        foreach (ScreenEffectData effect in effects) {
            ScreenEffectController.PlayEffect(effect, curve);
        }
    }

#if UNITY_EDITOR
    public void Draw()
    {
        EditorExtensions.DrawInLayout(false, "box", CEditor.window, true, false, () => {
            curve = EditorGUILayout.CurveField("Effects Curve", curve);

            if (effects == null) effects = new List<ScreenEffectData>();
            if (effects.Count == 0) effects.Add(new ScreenEffectData());
            for (int i = 0; i < effects.Count; i++) {
                ScreenEffectData effect = effects[i];
                EditorExtensions.DrawInLayout(true, true, false, () => {
                    effect.effectType = (ScreenEffectData.EffectType)EditorGUILayout.EnumPopup(effect.effectType);
                    if (effect.effectType == ScreenEffectData.EffectType.Aberration || effect.effectType == ScreenEffectData.EffectType.Bloom || effect.effectType == ScreenEffectData.EffectType.Saturation || effect.effectType == ScreenEffectData.EffectType.Lens) {
                        effect.from = EditorGUILayout.FloatField(effect.from, GUILayout.Width(40));
                        effect.to = EditorGUILayout.FloatField(effect.to, GUILayout.Width(40));
                        effect.duration = EditorGUILayout.FloatField(effect.duration, GUILayout.Width(40));
                        effect.priority = EditorGUILayout.IntField(effect.priority, GUILayout.Width(40));
                    } else {
                        effect.imageIndex = EditorGUILayout.IntField(effect.imageIndex, GUILayout.Width(40));
                        effect.colorFrom = EditorGUILayout.ColorField(effect.colorFrom, GUILayout.Width(40));
                        effect.colorTo = EditorGUILayout.ColorField(effect.colorTo, GUILayout.Width(40));
                        effect.duration = EditorGUILayout.FloatField(effect.duration, GUILayout.Width(40));
                    }
                    if (EditorExtensions.DrawColoredButton("+", CEditor.elementSelected, 20)) { effects.Add(new ScreenEffectData(effect)); return; }
                    if (EditorExtensions.DrawColoredButton("-", CEditor.elementSelected, 20)) { effects.Remove(effect); return; }
                });
            }
        });

        if (EditorExtensions.DrawColoredButton("Play", CEditor.element)) {
            Play();
        }
    }
#endif
}

#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(ScreenEffect))]
public class ScreenEffectEditor : Editor {
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        ScreenEffect o = target as ScreenEffect;
        o.Draw();
        EditorUtility.SetDirty(o);
        serializedObject.ApplyModifiedProperties();
    }
}
#endif