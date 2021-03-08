using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class RuleSet : ScriptableObject
{

	// ============================================================= VARIABLES

	// --------- PUBLIC
 

	// --------- PRIVATE


	// --------- BOOKMARKERS



	// ============================================================= LOCAL



	// ============================================================= CORPS

	

	// ============================================================= UTILITIES



	// ============================================================= EDITOR

	#if UNITY_EDITOR
	public virtual void Draw () {

	}
 	#endif
	
}

#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(RuleSet))]
public class RuleSetEditor : Editor {
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