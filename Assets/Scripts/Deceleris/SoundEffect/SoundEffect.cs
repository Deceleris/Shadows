using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
[CreateAssetMenu (menuName = "Content/Effect/SoundEffect", fileName = "SoundEffect")]
public class SoundEffect : ScriptableObject
{

    // ==================================== VARIABLES

    public AnimationCurve curve = default;
    public List<SoundEffectData> effects = new List<SoundEffectData>();

    public void Play()
    {
        foreach (SoundEffectData effect in effects) {
            SoundEffectController.PlayEffect(effect, curve, 1);
        }
    }

    public void PlayReverse()
    {
        foreach (SoundEffectData effect in effects) {
            SoundEffectController.PlayEffect(effect, curve, -1);
        }
    }

#if UNITY_EDITOR
    public void Draw () {
        EditorExtensions.DrawInLayout(false, "box", CEditor.window, true, false, () => {
            curve = EditorGUILayout.CurveField("Effects Curve", curve);

            if (effects == null) effects = new List<SoundEffectData>();
            if (effects.Count == 0) effects.Add(new SoundEffectData());
            for (int i = 0; i < effects.Count; i++) {
                SoundEffectData effect = effects[i];
                EditorExtensions.DrawInLayout(true, true, false, () => {
                    EditorGUILayout.BeginVertical();
                    EditorGUILayout.BeginHorizontal();
                    effect.parameter = EditorGUILayout.TextField("Parameter", effect.parameter);
                    if (EditorExtensions.DrawColoredButton("+", CEditor.elementSelected, 20)) { effects.Add(new SoundEffectData(effect)); return; }
                    if (EditorExtensions.DrawColoredButton("-", CEditor.elementSelected, 20)) { effects.Remove(effect); return; }
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginHorizontal();
                    effect.from = EditorGUILayout.FloatField(effect.from);
                    effect.to = EditorGUILayout.FloatField(effect.to);
                    effect.duration = EditorGUILayout.FloatField(effect.duration);
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.EndVertical();
                });
            }
        });

        if (EditorExtensions.DrawColoredButton("Play", CEditor.element))Play();
        if (EditorExtensions.DrawColoredButton("Reverse", CEditor.element))PlayReverse();
    }
 	#endif
	
}

#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(SoundEffect))]
public class SoundEffectEditor : Editor {
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        SoundEffect o = target as SoundEffect;
        o.Draw();
        EditorUtility.SetDirty(o);
        serializedObject.ApplyModifiedProperties();
    }
}
#endif