using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
[CreateAssetMenu (menuName = "Content/DataList", fileName = "DataList")]
public class DataList : ScriptableObject
{

	// ============================================================= VARIABLES

	// --------- PUBLIC
	[SerializeField] List<GameData> datas = new List<GameData> () {new GameData ("Default Data")};
 

	// --------- PRIVATE



	// --------- BOOKMARKERS



	// ============================================================= LOCAL



	// ============================================================= CORPS

	

	// ============================================================= UTILITIES

	public GameData Get (string name) {
		foreach (GameData data in datas) if (data.name == name) return data;
		return null;
	}

	public Color GetColor (string name){
		GameData data = Get (name);
		if (data != null) return data.colorValue;
		Debug.Log ("Data of name " + name + " don't exist in data list " + this.name + " ...");
		return Color.black;
	} 

	// ============================================================= EDITOR

	#if UNITY_EDITOR
	public void Draw (float labelWidth = 120, bool header = true) {
		// Header

		if (header) {
			EditorGUILayout.BeginHorizontal (EditorStyles.helpBox);
			GUILayout.Label ("Data List : " + this.name);
			EditorGUILayout.EndHorizontal (); 
		}

		EditorGUILayout.BeginVertical (GUILayout.ExpandHeight (true));
          for (int i = 0; i < datas.Count; i++) {

			if (datas[i] == null) datas[i] = new GameData ("Data Was Null");
               GameData data = datas[i];
               EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
			data.Draw (labelWidth);

               EditorGUILayout.BeginHorizontal(GUILayout.Width (100));
               if (EditorExtensions.DrawColoredButton("↓", Color.green * 0.5f, 20, 1)){
                    datas.Remove(data);
                    datas.Insert(Mathf.Clamp(i + 1, 0, datas.Count), data);
               }
               if (EditorExtensions.DrawColoredButton("+", Color.cyan * 0.5f, 20, 1)) {
                    datas.Insert(i + 1, new GameData ("New Data"));
               }
               if (EditorExtensions.DrawColoredButton("x", Color.red * 0.5f, 20, 2)) {
                    datas.RemoveAt(i);
               }
               EditorGUILayout.EndHorizontal();
               EditorGUILayout.EndHorizontal();
          }
		EditorGUILayout.EndVertical ();

		if (datas.Count == 0) {datas.Add (new GameData ("Default Data"));}
		if (GUILayout.Button("+ New Data")) {
			datas.Add (new GameData ("Data " + datas.Count));
		}
	}
 	#endif
	
}

#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(DataList))]
public class DataListEditor : Editor {
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DataList o = target as DataList;
        o.Draw();
        EditorUtility.SetDirty(o);
        serializedObject.ApplyModifiedProperties();
    }
}
#endif