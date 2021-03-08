using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class GameData 
{

     public string name;
     public DataType type = DataType.Int;

     public bool boolValue = true;
     public int intValue = 1;
     public float floatValue = 1;
     public string stringValue = "Text...";

     public Color colorValue = Color.white;
     public AnimationCurve curveValue = new AnimationCurve ();

     public GameData (string name) {
          this.name = name;
     }

#if UNITY_EDITOR

     [HideInInspector] public bool folded;

     public void Draw (float labelWidth = 120) {
          EditorExtensions.DrawInLayout (true, "button", CEditor.element, () => {
               type = (DataType)EditorGUILayout.EnumPopup (type, GUILayout.Width (60));
               name = EditorGUILayout.TextField (name, GUILayout.Width (labelWidth));

               if (type == DataType.Bool) boolValue = EditorGUILayout.Toggle (boolValue);
               if (type == DataType.Int) intValue = EditorGUILayout.IntField (intValue);
               if (type == DataType.Float) floatValue = EditorGUILayout.FloatField (floatValue);
               if (type == DataType.String) stringValue = EditorGUILayout.TextField (stringValue);

               if (type == DataType.Color) colorValue = EditorGUILayout.ColorField (colorValue);
               if (type == DataType.Curve) curveValue = EditorGUILayout.CurveField (curveValue);
          });
     }

#endif

}

public enum DataType {
     Bool,
     Int,
     Float,
     String,
     
     Color,
     Curve
}