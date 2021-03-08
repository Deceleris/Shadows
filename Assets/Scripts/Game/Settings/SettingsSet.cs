using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
[CreateAssetMenu (menuName = "Content/RuleSet/SettingsSet", fileName = "SettingsSet")]
public class SettingsSet : RuleSet
{

	// ============================================================= VARIABLES

	// --------- PUBLIC

	// *********** Séquences
	public float splashDuration = 3f;
	public float entryFadeDuraiton = 4f;

	// *********** Transition des scenes
	public float transitionTime = 1f;
	public AnimationCurve transitionCurve;

	// *********** Interface
	public float fadingDuration;
	public AnimationCurve fadingCurve;

	// *********** Options que le joueur pourra modifier
	public List<PlayerSetting> playerSettings =  new List<PlayerSetting> ();

	public static SettingsSet current;

	// ============================================================= LOCAL

	

	// ============================================================= CORPS

	

	// ============================================================= UTILITIES



	// ============================================================= EDITOR

	[SerializeField] [HideInInspector] int tab = 0;

	#if UNITY_EDITOR
	public override void Draw () {

		EditorExtensions.DrawInLayout (false, "box", CEditor.innerWindow, true, true, () => {

			// *********** HEADER
			EditorGUILayout.BeginHorizontal (EditorStyles.helpBox);
			GUILayout.Label ("Settings Editor | " + this.name.ToUpper(), EditorStyles.boldLabel);
			EditorGUILayout.EndHorizontal ();

			// *********** TABS
			EditorGUILayout.BeginHorizontal (EditorStyles.helpBox, GUILayout.ExpandWidth(false));
			if (EditorExtensions.DrawColoredButton ("Application", tab == 0 ? CEditor.elementSelected : CEditor.element, 100)) tab = 0;
			if (EditorExtensions.DrawColoredButton ("Player", tab == 1 ? CEditor.elementSelected : CEditor.element, 100)) tab = 1;
			EditorGUILayout.EndHorizontal ();

			// *********** TAB CONTENT
			if (tab == 0) {

				EditorGUILayout.BeginVertical(EditorStyles.helpBox);
				GUILayout.Label("SEQUENCES", EditorStyles.boldLabel);
				splashDuration = splashDuration.Draw ("Splash Duration");
				entryFadeDuraiton = entryFadeDuraiton.Draw ("Entry Fade Duration");
				EditorGUILayout.EndVertical();
				EditorGUILayout.Space();

				EditorGUILayout.BeginVertical (EditorStyles.helpBox);
				GUILayout.Label("SCENE", EditorStyles.boldLabel);
				transitionTime = transitionTime.Draw ("Transition Time");
				transitionCurve = EditorGUILayout.CurveField ("Transition Curve", transitionCurve);
				EditorGUILayout.EndVertical ();
				EditorGUILayout.Space();

				EditorGUILayout.BeginVertical(EditorStyles.helpBox);
				GUILayout.Label("INTERFACE", EditorStyles.boldLabel);
				fadingDuration = fadingDuration.Draw("Fading Duration");
				fadingCurve = EditorGUILayout.CurveField("Fading Curve", fadingCurve);
				EditorGUILayout.EndVertical();
				EditorGUILayout.Space();

			}
			else if (tab == 1) {
				playerSettings = EditorExtensions.DrawReordeableListAndContent<PlayerSetting> (playerSettings, "Datas", (PlayerSetting setting) => {setting.Draw ();});
				if (playerSettings.Count == 0) {playerSettings.Add (new PlayerSetting ());}

				if (GUILayout.Button("+ New Data")) {
					playerSettings.Add (new PlayerSetting ());
				}
			}
          });

	}
 	#endif
	
}

#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(SettingsSet))]
public class SettingsSetEditor : Editor {
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        SettingsSet o = target as SettingsSet;
        o.Draw();
        EditorUtility.SetDirty(o);
        serializedObject.ApplyModifiedProperties();
    }
}
#endif