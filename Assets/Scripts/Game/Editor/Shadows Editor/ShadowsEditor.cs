using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ShadowsEditor : EditorWindow
{

	// ============================================================= VARIABLES

    const string code = "Shadows";
    const string style = "box";

    [SerializeField] bool selectingGameSet = true;
    [SerializeField] GameSet gameSet;

	// ============================================================= LOCAL

    [MenuItem("Shadows/Shadows Editor")]
    public static void Open () {
        ShadowsEditor window = (ShadowsEditor)EditorWindow.GetWindow(typeof(ShadowsEditor));
        window.titleContent = new GUIContent ("Shadows Editor");
        window.selectingGameSet = true;
    }   
    
    public void OnGUI () {
        Draw ();
    }

	// ============================================================= CORPS

    public void Draw () {
        EditorExtensions.DrawInLayout (true, style, CEditor.backgroundColor, true, true, () => {

            Texture2D background = Resources.Load<Texture2D> ("EditorTextures/Background");
            background.wrapMode = TextureWrapMode.Repeat;
            Rect screen = new Rect (10, 10, Screen.width - 20, Screen.height - 40);
            float size = 10;
            GUI.DrawTextureWithTexCoords(screen, background, new Rect(0, 0, screen.width / background.width * size, screen.height / background.height * size));

            selectingGameSet = gameSet == null ? true : selectingGameSet;

            if (selectingGameSet)
                GameSetsSelection ();
            else 
                GameSetEdition ();

        });
    }

    public void GameSetEdition () {
        gameSet.DrawEditor (()=> {
            selectingGameSet = true;
        });
    }

    public void GameSetsSelection () {
        EditorExtensions.CenteredArea (600, 300, () => {
            EditorExtensions.DrawInLayout (false, style, CEditor.window, true, true, () => {
                
                // ================================ TITLE

                EditorExtensions.CenteredTitle ("GAME SET", 20);

                // =================

                EditorGUILayout.BeginVertical (GUILayout.ExpandHeight (true), GUILayout.ExpandWidth (true));
                EditorGUILayout.BeginHorizontal ();

                // ================================ GAME SET LIST

                gameSet = EditorExtensions.DrawHierarchyOfScriptableObject<GameSet> (gameSet, "GameSets", 200);

                // ================================ GAME SET INSPECTOR

                if (gameSet != null) {
                    gameSet.DrawInspector (()=> {
                        AssetDatabase.DeleteAsset ("Assets/Resources/GameSets/" + gameSet.name + ".asset");
                    }, ()=> {   
                        selectingGameSet = false;
                    });
                }

                // =================

                EditorGUILayout.EndHorizontal ();
                EditorGUILayout.EndVertical ();

            });
        });
    }
}