using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class PlayerSetting 
{

// ============================================================= VARIABLES

     public string name;
     public SettingType type;

     public float floatValue;
     
// ============================================================= EDITOR

#if UNITY_EDITOR
	public void Draw () {
		EditorExtensions.DrawInLayout (false, "box", CEditor.element, () => {
               name = name.Draw ("Name");
               type = (SettingType)EditorGUILayout.EnumPopup ("Type", type);
          });
	}
#endif

}

public enum SettingType {
     Slider
}