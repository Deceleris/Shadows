using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class RFloat
{

    [SerializeField] bool random = false;
    [SerializeField] float defaultValue = 1;
    [SerializeField] float minValue = 0.8f;
    [SerializeField] float maxValue = 1.2f;

    public float sort { get {
            return
            random ? Random.Range(minValue, maxValue) :
            defaultValue;
    } }

#if UNITY_EDITOR

    public void DrawField (string label)
    {
        EditorGUILayout.BeginHorizontal();

        GUI.backgroundColor = random ? Color.gray : Color.white;
        if (GUILayout.Button(label)) random = !random;
        GUI.backgroundColor = Color.white;

        if (random) {
            minValue = EditorGUILayout.FloatField(minValue);
            maxValue = EditorGUILayout.FloatField(maxValue);
        }
        else {
            defaultValue = EditorGUILayout.FloatField(defaultValue);
        }

        EditorGUILayout.EndHorizontal();
    }

#endif

}