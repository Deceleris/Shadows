using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TypeReferences;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
[CreateAssetMenu(menuName = "Content/GameSet", fileName = "GameSet")]
public class GameSet : ScriptableObject
{

    // ============================================================= VARIABLES

    const string style = "box";

    [SerializeField] SettingsSet settings;
    [SerializeField] EnemiesSet enemies;

    [SerializeField] bool officialGameSet;
    [SerializeField] ClassTypeReference ruleSetSelected;

    public static GameSet current;

    // ============================================================= LOCAL



    // ============================================================= CORPS



    // ============================================================= UTILITIES

    public static void SetGameSet(GameSet gameSet)
    {
        current = gameSet;
        SettingsSet.current = gameSet.settings;
    }

    // ============================================================= EDITOR

#if UNITY_EDITOR
    public void Draw()
    {

    }

    public void DrawInspector(System.Action delete, System.Action select)
    {
        EditorExtensions.DrawInLayout(false, style, CEditor.innerWindow, true, true, () => {

            EditorExtensions.CenteredTitle(name, 15);

            // ================================ CONTENT

            EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            EditorExtensions.DrawHorizontalLine(CEditor.innerWindow);

            EditorGUIUtility.labelWidth += 45;
            officialGameSet = officialGameSet.Draw("Official Game Set");
            EditorGUIUtility.labelWidth -= 45;

            EditorGUILayout.EndVertical();

            // ================================ BUTTONS

            EditorGUILayout.BeginHorizontal();
            EditorGUI.BeginDisabledGroup(officialGameSet);
            if (GUILayout.Button("Delete")) {
                delete();
            }
            EditorGUI.EndDisabledGroup();
            if (EditorExtensions.DrawColoredButton("Edit", CEditor.elementSelected)) {
                select();
            }
            EditorGUILayout.EndHorizontal();

        });
    }

    public void DrawEditor(System.Action change)
    {
        EditorGUILayout.BeginHorizontal();

        // ================================ Rule Sets of the game set

        EditorGUILayout.BeginVertical(GUILayout.Width(210));
        EditorExtensions.DrawInLayout(false, "", CEditor.backgroundColor, true, true, () => {
            EditorExtensions.DrawInLayout(false, style, CEditor.innerWindow, () => {
                EditorExtensions.CenteredTitle(this.name.ToUpper (), 15);

                if (GUILayout.Button("Change Game Set")) {
                    change();
                }
            });

            // List

            EditorExtensions.DrawInLayout(false, style, CEditor.backgroundColor, true, true, () => {
                if (EditorExtensions.DrawColoredButton("Settings", ruleSetSelected == typeof(SettingsSet) ? CEditor.elementSelected : CEditor.element)) ruleSetSelected = typeof(SettingsSet);
                if (EditorExtensions.DrawColoredButton("Enemies", ruleSetSelected == typeof(EnemiesSet) ? CEditor.elementSelected : CEditor.element)) ruleSetSelected = typeof(EnemiesSet);
            });
        });

        // ================================ Rule Sets list 

        if (ruleSetSelected != null) {
            EditorExtensions.DrawInLayout(false, "", CEditor.backgroundColor, true, true, () => {
                if (ruleSetSelected == typeof(SettingsSet))
                    settings = EditorExtensions.DrawHierarchyOfScriptableObject<SettingsSet>(settings, "RuleSets/SettingsSets", 200, true);
                if (ruleSetSelected == typeof(EnemiesSet))
                    enemies = EditorExtensions.DrawHierarchyOfScriptableObject<EnemiesSet>(enemies, "RuleSets/EnemiesSets", 200, true);
            });
        }
        EditorGUILayout.EndVertical();

        // ================================ Rule Set Editor 

        if (ruleSetSelected != null) {
            if (ruleSetSelected == typeof(SettingsSet) && settings != null)
                settings.Draw();
            if (ruleSetSelected == typeof(EnemiesSet) && enemies != null)
                enemies.Draw();
        }

        EditorGUILayout.EndHorizontal();
    }
#endif

}

#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(GameSet))]
public class GameSetEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        GameSet o = target as GameSet;
        o.Draw();
        EditorUtility.SetDirty(o);
        serializedObject.ApplyModifiedProperties();
    }
}
#endif