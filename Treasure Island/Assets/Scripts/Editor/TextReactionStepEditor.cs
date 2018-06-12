using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TextReactionStep))]
public class TextReactionStepEditor : Editor {

    SerializedProperty textProp;

    private void OnEnable()
    {
        textProp = serializedObject.FindProperty("text");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GUIStyle style = new GUIStyle(GUI.skin.textArea);
        style.clipping = TextClipping.Clip;

        EditorGUILayout.LabelField("Text :");
        textProp.stringValue = EditorGUILayout.TextArea(textProp.stringValue, style, GUILayout.Height(80));

        serializedObject.ApplyModifiedProperties();
    }


}
