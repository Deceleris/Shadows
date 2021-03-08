using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
[CreateAssetMenu (menuName = "Content/RuleSet/EnemiesSet", fileName = "EnemiesSet")]
public class EnemiesSet : RuleSet
{

	// ============================================================= VARIABLES

	// --------- PUBLIC

	public int setting;

	// --------- PRIVATE



	// --------- BOOKMARKERS



	// ============================================================= LOCAL



	// ============================================================= CORPS

	

	// ============================================================= UTILITIES



	// ============================================================= EDITOR

	#if UNITY_EDITOR
	public override void Draw () {
		setting = setting.Draw ();
	}
 	#endif
	
}

#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(EnemiesSet))]
public class EnemiesSetEditor : Editor {
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EnemiesSet o = target as EnemiesSet;
        o.Draw();
        EditorUtility.SetDirty(o);
        serializedObject.ApplyModifiedProperties();
    }
}
#endif