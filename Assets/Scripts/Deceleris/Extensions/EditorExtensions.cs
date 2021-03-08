using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static class EditorExtensions
{
#if UNITY_EDITOR

    // --------------------------------------------- BASIC TYPES FIELDS

    // --------------------------------------------- FLOAT

    public static float DrawColoredFloat (float value, string label, Color color, int size)
    {
        float returnValue = value;
        GUI.backgroundColor = color;
        returnValue = EditorGUILayout.FloatField(label, value, GUILayout.Width(size));
        GUI.backgroundColor = Color.white;
        return returnValue;
    }

    public static float DrawFloat(float value, string label, int size)
    {
        float returnValue = value;
        returnValue = EditorGUILayout.FloatField(label, value, GUILayout.Width(size));
        return returnValue;
    }

    public static float DrawFloat(float value, int size)
    {
        float returnValue = value;
        returnValue = EditorGUILayout.FloatField(value, GUILayout.Width(size));
        return returnValue;
    }

    public static float Draw(this float value, string name = "Value") {
        return EditorGUILayout.FloatField(name, value);
    }

    // --------------------------------------------- INT

    public static int Draw (this int value, string label = "Value") {
        return EditorGUILayout.IntField (label, value);
    }

    public static int DrawInt(int value, int size)
    {
        int returnValue = value;
        returnValue = EditorGUILayout.IntField(value, GUILayout.Width(size));
        return returnValue;
    }

    // --------------------------------------------- TEXT

    public static string Draw (this string value, string label = "Value") {
        return EditorGUILayout.TextField (label, value);
    }

    public static string DrawArea(this string value, int height) {
        return EditorGUILayout.TextArea(value, GUILayout.Height(height));
    }

    public static string DrawArea(this string value, string label, int height) {
        GUILayout.Label(label);
        return EditorGUILayout.TextArea(value, GUILayout.Height(height));
    }

    // --------------------------------------------- COLOR

    public static Color Draw(this Color value, string name = "Value") {
        return EditorGUILayout.ColorField(name, value);
    }

    public static Color DrawWithoutLabel(this Color value) {
        return EditorGUILayout.ColorField(value);
    }

    // --------------------------------------------- BOOL

    public static bool Draw (this bool value, string label = "Value") {
        return EditorGUILayout.Toggle (label, value);
    }

    // Cette fonction dessine un bouton toggle, qui switch le statut d'une booléan
    public static bool ToggleButton (bool value, Color color, string label) 
    {
        if (value) GUI.backgroundColor = color;;
        if (GUILayout.Button (label, GUILayout.Height (20))) {return !value;}
        GUI.backgroundColor = Color.white;
        
        return value;
    }

    // Cette fonction dessine un bouton toggle, qui switch le statut d'une booléan
    public static bool ToggleButton (bool value, Color color, string label, float height, float width) 
    {
        if (value) GUI.backgroundColor = color;;
        if (GUILayout.Button (label, GUILayout.Height (height), GUILayout.Width (width))) {return !value;}
        GUI.backgroundColor = Color.white;
        
        return value;
    }

    // --------------------------------------------- VECTOR

    public static Vector2 DrawNormalField (this Vector2 from, string label)
    {
        Vector2 returned = from;
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label(label, GUILayout.Width(116));
        returned.x = EditorGUILayout.FloatField(returned.x);
        returned.y = EditorGUILayout.FloatField(returned.y);
        EditorGUILayout.EndHorizontal();
        return returned;
    }

    public static Vector3 DrawNormalField (this Vector3 from, string label)
    {
        Vector3 returned = from;
        GUILayout.BeginHorizontal();
        GUILayout.Label(label, GUILayout.Width(116));
        returned.x = EditorGUILayout.FloatField(returned.x, GUILayout.Width(40));
        returned.y = EditorGUILayout.FloatField(returned.y, GUILayout.Width(40));
        returned.z = EditorGUILayout.FloatField(returned.z, GUILayout.Width(40));
        GUILayout.EndHorizontal();
        return returned;
    }

    // --------------------------------------------- TITLE

    public static void CenteredTitle (string label, int fontSize) {
        EditorGUILayout.BeginHorizontal ();
        GUILayout.FlexibleSpace ();
        int prev = EditorStyles.boldLabel.fontSize;
        EditorStyles.boldLabel.fontSize = fontSize;
        GUILayout.Label (label, EditorStyles.boldLabel);
        EditorStyles.boldLabel.fontSize = prev;
        GUILayout.FlexibleSpace ();
        EditorGUILayout.EndHorizontal ();
    }

    // --------------------------------------------- OBJECT

    public static T DrawPickableSquareField<T> (T value, int size, bool allowSceneObject = false) where T : Object
    {
        return (T)EditorGUILayout.ObjectField(value, typeof(T), allowSceneObject, GUILayout.Width (size), GUILayout.Height (size));
    }
    
    public static void DrawPingSelectButton (this Object from)
    {
        if (GUILayout.Button ("◄┐└►"))
        {
            EditorGUIUtility.PingObject(from);
            Selection.activeObject = from;
        }
    }

    // --------------------------------------------- BUTTON

    public static void DrawColoredButton (string label, Color color, int size, System.Action action)
    {
        GUI.backgroundColor = color;
        if (GUILayout.Button(label, GUILayout.Width(size))) action();
        GUI.backgroundColor = Color.white;
    }

    public static void DrawColoredButton (string label, Color color, System.Action action)
    {
        GUI.backgroundColor = color;
        if (GUILayout.Button(label)) action();
        GUI.backgroundColor = Color.white;
    }

    public static bool DrawColoredButton(string label, Color color, int size)
    {
        bool pressed = false;
        GUI.backgroundColor = color;
        if (GUILayout.Button(label, GUILayout.Width(size))) pressed = true;
        GUI.backgroundColor = Color.white;
        return pressed;
    }

    public static bool DrawColoredButton(string label, Color color)
    {
        bool pressed = false;
        GUI.backgroundColor = color;
        if (GUILayout.Button(label)) pressed = true;
        GUI.backgroundColor = Color.white;
        return pressed;
    }

    public static bool DrawColoredButton(string label, Color color, int size, int side)
    {
        bool pressed = false;
        GUI.backgroundColor = color;
        if (GUILayout.Button(label, side == 0 ? EditorStyles.miniButtonLeft : side == 1 ? EditorStyles.miniButtonMid : side == 2 ? EditorStyles.miniButtonRight : "buton", GUILayout.Width(size))) pressed = true;
        GUI.backgroundColor = Color.white;
        return pressed;
    }

    public static void DrawColoredButton(string label, Color color, int size, int side, System.Action action)
    {
        GUI.backgroundColor = color;
        if (GUILayout.Button(label, side == 0 ? EditorStyles.miniButtonLeft : side == 1 ? EditorStyles.miniButtonMid : side == 2 ? EditorStyles.miniButtonRight : "buton", GUILayout.Width(size))) action();
        GUI.backgroundColor = Color.white;
    }

    // --------------------------------------------- FOLDER

    public static bool FoldoutLabel (bool isFolded, string label)
    {
        bool returnedValue = isFolded;
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button((isFolded ? "►" : "▼") + " " + label, EditorStyles.boldLabel)) returnedValue = !returnedValue;
        EditorGUILayout.EndHorizontal();
        return returnedValue;
    }

    // --------------------------------------------- GRAPHICS

    public static void DrawHorizontalLine (Color color, int size = 1, string style = "button")
    {
        GUI.backgroundColor = color;
        EditorGUILayout.BeginHorizontal(style, GUILayout.ExpandWidth (true), GUILayout.Height (size));
        GUI.backgroundColor = Color.white;
        EditorGUILayout.EndHorizontal();
    }

    // --------------------------------------------- LAYOUTING

    public static void BeginVerticalBox (Color color, bool fullExtanded = false) 
    {
        GUI.backgroundColor = color;
        if (!fullExtanded)
            EditorGUILayout.BeginVertical ("box");
        else
            EditorGUILayout.BeginVertical ("box", GUILayout.ExpandWidth (true), GUILayout.ExpandHeight (true));
        GUI.backgroundColor = Color.white;
    }

    public static void DrawInLayout (bool horizontal, string style, Color color, System.Action drawContent) 
    {
        GUI.backgroundColor = color;
        if (horizontal) EditorGUILayout.BeginHorizontal(style);
        else EditorGUILayout.BeginVertical(style);
        GUI.backgroundColor = Color.white;

        drawContent();

        if (horizontal) EditorGUILayout.EndHorizontal(); else EditorGUILayout.EndVertical();
    }

    public static void DrawInLayout (bool horizontal, string style, Color color, bool extandWidth, bool extandheight, System.Action drawContent) 
    {
        GUI.backgroundColor = color;
        if (horizontal) EditorGUILayout.BeginHorizontal(style, GUILayout.ExpandWidth(extandWidth), GUILayout.ExpandHeight(extandheight));
        else EditorGUILayout.BeginVertical(style, GUILayout.ExpandWidth(extandWidth), GUILayout.ExpandHeight(extandheight));
        GUI.backgroundColor = Color.white;

        drawContent();

        if (horizontal) EditorGUILayout.EndHorizontal(); else EditorGUILayout.EndVertical();
    }

    public static void DrawInLayout (bool horizontal, string style, Color color, float width, float height, System.Action drawContent) 
    {
        GUI.backgroundColor = color;
        if (horizontal) EditorGUILayout.BeginHorizontal(style, GUILayout.Width(width), GUILayout.Height(height));
        else EditorGUILayout.BeginVertical(style, GUILayout.Width(width), GUILayout.Height(height));
        GUI.backgroundColor = Color.white;

        drawContent();

        if (horizontal) EditorGUILayout.EndHorizontal(); else EditorGUILayout.EndVertical();
    }
    
    public static void DrawInLayout (bool horizontal, string style, Color color, float width, bool extandheight, System.Action drawContent) 
    {
        GUI.backgroundColor = color;
        if (horizontal) EditorGUILayout.BeginHorizontal(style, GUILayout.Width(width), GUILayout.ExpandHeight(extandheight));
        else EditorGUILayout.BeginVertical(style, GUILayout.Width(width), GUILayout.ExpandHeight(extandheight));
        GUI.backgroundColor = Color.white;

        drawContent();

        if (horizontal) EditorGUILayout.EndHorizontal(); else EditorGUILayout.EndVertical();
    }

    public static void DrawInLayout (bool horizontal, string style, Color color, bool extandWidth, float height, System.Action drawContent) 
    {
        GUI.backgroundColor = color;
        if (horizontal) EditorGUILayout.BeginHorizontal(style, GUILayout.ExpandWidth(extandWidth), GUILayout.Height(height));
        else EditorGUILayout.BeginVertical(style, GUILayout.ExpandWidth(extandWidth), GUILayout.Height(height));
        GUI.backgroundColor = Color.white;

        drawContent();

        if (horizontal) EditorGUILayout.EndHorizontal(); else EditorGUILayout.EndVertical();
    }

    public static void DrawInLayout (bool horizontal, bool extandWidth, bool extandheight, System.Action drawContent) 
    {
        if (horizontal) EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(extandWidth), GUILayout.ExpandHeight(extandheight));
        else EditorGUILayout.BeginVertical( GUILayout.ExpandWidth(extandWidth), GUILayout.ExpandHeight(extandheight));

        drawContent();

        if (horizontal) EditorGUILayout.EndHorizontal(); else EditorGUILayout.EndVertical();
    }

    // --------------------------------------------- AREA

    public static void Padding (float padding, System.Action drawContent) {
        GUILayout.BeginArea (new Rect (padding, padding, Screen.width - padding * 2, Screen.height - padding * 2));
        drawContent();
        GUILayout.EndArea ();
    }

    public static void Padding (float left, float right, float top, float bottom, System.Action drawContent) {
        GUILayout.BeginArea (new Rect (left, top, Screen.width - (left + right), Screen.height - (bottom + top)));
        GUILayout.EndArea ();
    }

    public static void CenteredArea (float width, float height, System.Action drawContent) {
        GUILayout.BeginArea (new Rect((Screen.width / 2) - width / 2, (Screen.height / 2) - height / 2, width, height));
        drawContent();
        GUILayout.EndArea ();
    }

    // --------------------------------------------- LISTING

    public static List<T> DrawReordeableList<T> (this List<T> elements, string label, bool allowSceneObject = false) where T : Object
    {
        for (int i = 0; i < elements.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();

            if (typeof(T) == typeof(Sprite))
            {
                EditorGUILayout.BeginHorizontal();
                elements[i] = DrawPickableSquareField<T>(elements[i], 50, allowSceneObject);
                GUILayout.Label(label + " " + (i + 1).ToString());
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                elements[i] = (T)EditorGUILayout.ObjectField(label + " " + (i + 1).ToString(), elements[i], typeof(T), allowSceneObject);
            }
            if (DrawColoredButton ("▼", Color.green * 0.5f, 20, 1))
            {
                T element = elements[i];
                elements.Remove(element);
                elements.Insert(Mathf.Clamp(i + 1, 0, elements.Count), element);
            }
            if (DrawColoredButton("+", Color.cyan * 0.5f, 20, 1)) {
                elements.Insert(i+1, null);
            }
            if (DrawColoredButton ("x", Color.red * 0.5f, 20, 2))
            {
                elements.RemoveAt(i);
            }

            EditorGUILayout.EndHorizontal();            
        }

        return elements;
    }

    public static List<T> DrawReordeableList<T>(this List<T> elements, string label, int labelWidth) where T : Object
    {
        for (int i = 0; i < elements.Count; i++) {
            EditorGUILayout.BeginHorizontal();

            if (typeof(T) == typeof(Sprite)) {
                EditorGUILayout.BeginHorizontal();
                elements[i] = DrawPickableSquareField<T>(elements[i], 50);
                GUILayout.Label(label + " " + (i + 1).ToString(), GUILayout.Width (labelWidth));
                EditorGUILayout.EndHorizontal();
            }
            else {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(label + " " + (i + 1).ToString(), GUILayout.Width (labelWidth));
                elements[i] = (T)EditorGUILayout.ObjectField(elements[i], typeof(T), false);
                EditorGUILayout.EndHorizontal();
            }
            if (DrawColoredButton("▼", Color.green * 0.5f, 20, 1)) {
                T element = elements[i];
                elements.Remove(element);
                elements.Insert(Mathf.Clamp(i + 1, 0, elements.Count), element);
            }
            if (DrawColoredButton("+", Color.cyan * 0.5f, 20, 1)) {
                elements.Insert(i+1, null);
            }
            if (DrawColoredButton("x", Color.red * 0.5f, 20, 2)) {
                elements.RemoveAt(i);
            }

            EditorGUILayout.EndHorizontal();
        }

        return elements;
    }

    public static void SimpleDrawReordeableList<T>(this List<T> elements, string label) where T : Object
    {
        if (elements.Count == 0) return;
        for (int i = 0; i < elements.Count; i++) {
            EditorGUILayout.BeginHorizontal();

            if (typeof(T) == typeof(Sprite)) {
                EditorGUILayout.BeginHorizontal();
                DrawPickableSquareField<T>(elements[i], 50);
                GUILayout.Label(label + " " + (i + 1).ToString());
                EditorGUILayout.EndHorizontal();
            }
            else {
                EditorGUILayout.ObjectField(label + " " + (i + 1).ToString(), elements[i], typeof(T), false);
            }

            if (DrawColoredButton("▲", Color.green * 0.5f, 20, 0)) {
                T element = elements[i];
                elements.Remove(element);
                elements.Insert(Mathf.Clamp(i - 1, 0, elements.Count), element);
            }
            if (DrawColoredButton("▼", Color.green * 0.5f, 20, 1)) {
                T element = elements[i];
                elements.Remove(element);
                elements.Insert(Mathf.Clamp(i + 1, 0, elements.Count), element);
            }

            EditorGUILayout.EndHorizontal();
        }
    }

    public static List<T> DrawReordeableListAndContent<T>(this List<T> elements, string label, System.Action<T> draw) where T : class
    {
        for (int i = 0; i < elements.Count; i++)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            EditorGUILayout.BeginHorizontal();

            GUILayout.Label(label + " " + (i+1), EditorStyles.boldLabel);
            if (DrawColoredButton("▼", Color.green * 0.5f, 20, 1))
            {
                T element = elements[i];
                elements.Remove(element);
                elements.Insert(Mathf.Clamp(i + 1, 0, elements.Count), element);
            }
            if (DrawColoredButton("+", Color.cyan * 0.5f, 20, 1)) {
                elements.Insert(i+1, null);
            }
            if (DrawColoredButton("x", Color.red * 0.5f, 20, 2))
            {
                elements.RemoveAt(i);
                return elements;
            }
            EditorGUILayout.EndHorizontal();

            draw(elements[i]);

            EditorGUILayout.EndHorizontal();
        }

        return elements;
    }

    static bool editName;
    static string newName;

    public static T DrawHierarchyOfScriptableObject<T> (T selected, string path, int size, bool allowDelete = false, bool allowClone = true) where T : ScriptableObject {

        T [] elements = CEditor.GetAllScriptableOfType<T> ();
        T newSelected = selected;

        if (elements.Length != 0) {
            EditorExtensions.DrawInLayout (false, "box", CEditor.backgroundColor, size, true, ()=> {
                EditorGUILayout.BeginVertical (GUILayout.ExpandHeight (true), GUILayout.ExpandWidth (true));
                for (int i = 0; i < elements.Length; i++) {
                    T element = elements[i];

                    if (selected != element) {
                        EditorExtensions.DrawColoredButton (element.name, CEditor.element, 190, () => {
                            newSelected = element;
                            Selection.objects = new Object [] {element};
                            EditorGUIUtility.PingObject (element);
                            editName = false;
                        });
                    } else {
                        EditorGUILayout.BeginHorizontal ();
                        if (!editName) {
                                EditorExtensions.DrawColoredButton (element.name, CEditor.elementSelected, 190, () => {
                                editName = true;
                                newName = element.name;
                            });
                        }
                        else {
                            newName = EditorGUILayout.TextField (newName);
                            if (EditorExtensions.DrawColoredButton ("✎", CEditor.elementSelected, 25)) {
                                AssetDatabase.RenameAsset (AssetDatabase.GetAssetPath (element), newName);
                                AssetDatabase.SaveAssets ();
                                editName = false;
                            }
                            if (allowClone) {
                                if (EditorExtensions.DrawColoredButton("+", CEditor.elementSelected, 25)) {
                                    T clone = ScriptableObject.Instantiate<T>(element);
                                    clone.name = clone.name + " Copie";
                                    AssetDatabase.CreateAsset(clone, AssetDatabase.GetAssetPath(element).Replace(".asset", " Copie.asset"));
                                    AssetDatabase.SaveAssets();
                                    elements = CEditor.GetAllScriptableOfType<T> ();
                                    newSelected = clone;
                                    newName = newSelected.name;
                                    editName = false;
                                }
                            }
                            if (allowDelete) {
                                if (EditorExtensions.DrawColoredButton ("-", CEditor.elementSelected, 25)) {
                                    AssetDatabase.DeleteAsset (AssetDatabase.GetAssetPath (element));
                                    elements = CEditor.GetAllScriptableOfType<T> ();
                                    newSelected = elements[Mathf.Clamp (i - 1, 0, elements.Length - 1)];
                                    newName = newSelected.name;
                                    editName = false;
                                }
                            }
                        }
                        EditorGUILayout.EndHorizontal ();
                    }

                }
                EditorGUILayout.EndVertical ();

                EditorGUILayout.BeginHorizontal ();
                if (GUILayout.Button ("Create") || elements.Length == 0) {
                    T newElement = (T)CEditor.CreateScriptableObject<T> (path + "/New " + typeof(T).ToString() + (elements.Length + 1) + ".asset");
                    elements = CEditor.GetAllScriptableOfType<T> ();
                    newSelected = newElement;
                }
                EditorGUILayout.EndHorizontal ();
            });
        } 
        else {
            EditorExtensions.DrawInLayout (false, "button", CEditor.innerWindow, true, true, () => {
                if (GUILayout.Button ("Create")) {
                    T newElement = (T)CEditor.CreateScriptableObject<T> (path + "/New " + typeof(T).ToString() + (elements.Length + 1) + ".asset");
                    elements = CEditor.GetAllScriptableOfType<T> ();
                    newSelected = newElement;
                }
            });
        }

        return newSelected;
    }

    public static T DrawHierarchyOfScriptableObject<T>(List<T> elements, T selected, int size, bool allowDelete = false, bool allowClone = true) where T : ScriptableObject
    {

        T newSelected = selected;

        if (elements.Count != 0) {
            EditorGUILayout.BeginVertical();
            for (int i = 0; i < elements.Count; i++) {
                T element = elements[i];

                if (selected != element) {
                    EditorExtensions.DrawColoredButton(element.name, CEditor.element, 190, () => {
                        newSelected = element;
                        Selection.objects = new Object[] { element };
                        EditorGUIUtility.PingObject(element);
                        editName = false;
                    });
                }
                else {
                    EditorGUILayout.BeginHorizontal();
                    if (!editName) {
                        EditorExtensions.DrawColoredButton(element.name, CEditor.elementSelected, 190, () => {
                            editName = true;
                            newName = element.name;
                        });
                    }
                    else {
                        newName = EditorGUILayout.TextField(newName);
                        if (EditorExtensions.DrawColoredButton("✎", CEditor.elementSelected, 25)) {
                            element.name = newName;
                            editName = false;
                        }
                        if (allowClone) {
                            if (EditorExtensions.DrawColoredButton("+", CEditor.elementSelected, 25)) {
                                T clone = ScriptableObject.Instantiate<T>(element);
                                clone.name = clone.name + " Copie";                                
                                newSelected = clone;
                                elements.Add(clone);
                                newName = newSelected.name;
                                editName = false;
                                return newSelected;
                            }
                        }
                        if (allowDelete) {
                            if (EditorExtensions.DrawColoredButton("-", CEditor.elementSelected, 25)) {
                                elements.Remove(element);
                                newSelected = elements[Mathf.Clamp(i - 1, 0, elements.Count - 1)];
                                newName = newSelected.name;
                                editName = false;
                                return newSelected;
                            }
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Create") || elements.Count == 0) {
                T newElement = ScriptableObject.CreateInstance<T>();
                newElement.name = "New " + typeof(T).ToString ();
                elements.Add(newElement);
                newSelected = newElement;
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
        else {
            EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true), GUILayout.ExpandHeight (true));
            if (GUILayout.Button("Create")) {
                T newElement = ScriptableObject.CreateInstance<T>();
                elements.Add(newElement);
                newSelected = newElement;
            }
            EditorGUILayout.EndVertical();
        }

        return newSelected;
    }

#endif
}