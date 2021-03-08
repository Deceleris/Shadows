using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CEditor
{    

     static DataList _editorColors;

     public static DataList editorColors { get {
          if (_editorColors == null) _editorColors = Resources.Load<DataList> ("DataLists/EditorColors");
          if (_editorColors == null) Debug.Log ("The Editor Colors DataList at 'Assets/Resources/DataLists/EditorColors' doesn't exist");
          return _editorColors;
     }}

     public static Color backgroundColor {get{return editorColors.GetColor ("Background");}}
     public static Color window {get{return editorColors.GetColor ("Window");}}
     public static Color innerWindow {get{return editorColors.GetColor ("Inner Window");}}
     public static Color element {get{return editorColors.GetColor ("Element");}}
     public static Color elementSelected {get{return editorColors.GetColor ("Element Selected");}}

     public static T[] GetAllScriptableOfType<T> () where T : ScriptableObject {
          return Resources.LoadAll<T> ("");
     }

     #if UNITY_EDITOR

     public static T CreateScriptableObject<T> (string path) where T : ScriptableObject {
          T instance = ScriptableObject.CreateInstance<T> ();
          UnityEditor.AssetDatabase.CreateAsset (instance, "Assets/Resources/" + path);
          return instance;
     }

     #endif

}