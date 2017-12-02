using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class UIEditorHelper{

    public static void DrawTab(string text,ref bool state) {
        if (state) {
            text = "\u25BC " + text;
        }
        else {
            text = "\u25BA " + text;
        }
        if (!GUILayout.Toggle(true,text,"dragtab")) { state = !state; }
    }

    public static void BeginBox(string skin= "Box") {
        EditorGUILayout.BeginVertical(skin);
    }

    public static void BeginBox(Color c, string skin = "Box") {
        GUI.backgroundColor = c;
        EditorGUILayout.BeginVertical(skin);
    }

    public static void EndBox() {
        EditorGUILayout.EndVertical();
        GUI.backgroundColor = Color.white;
    }
}
