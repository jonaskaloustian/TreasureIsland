using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GlobalValues))]
public class GlobalValuesEditor : Editor{

    private SerializedProperty defaultValuesProp;
    private SerializedProperty criticalValuesBoolsProp;
    private SerializedProperty criticalValuesEndTextsProp;
    int valueEnumSize;

    void OnEnable()
    {
        defaultValuesProp = serializedObject.FindProperty("defaultValues");
        criticalValuesBoolsProp = serializedObject.FindProperty("criticalValuesBools");
        criticalValuesEndTextsProp = serializedObject.FindProperty("criticalValuesEndTexts");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        valueEnumSize = System.Enum.GetNames(typeof(Resource)).Length;
        defaultValuesProp.arraySize = valueEnumSize;
        criticalValuesBoolsProp.arraySize = valueEnumSize;
        criticalValuesEndTextsProp.arraySize = valueEnumSize;

        GUIStyle textAreaStyle = new GUIStyle(GUI.skin.textArea);
        textAreaStyle.clipping = TextClipping.Clip;

        for (int i = 0; i < defaultValuesProp.arraySize; i++)
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUI.indentLevel++;

            EditorGUILayout.LabelField(System.Enum.GetNames(typeof(Resource))[i]);
            defaultValuesProp.GetArrayElementAtIndex(i).intValue = EditorGUILayout.IntField("Value", defaultValuesProp.GetArrayElementAtIndex(i).intValue);
            criticalValuesBoolsProp.GetArrayElementAtIndex(i).boolValue = EditorGUILayout.Toggle("Critical?", criticalValuesBoolsProp.GetArrayElementAtIndex(i).boolValue);
            if (criticalValuesBoolsProp.GetArrayElementAtIndex(i).boolValue)
            {
                EditorGUILayout.LabelField("Ending Text :");
                criticalValuesEndTextsProp.GetArrayElementAtIndex(i).stringValue = EditorGUILayout.TextArea(criticalValuesEndTextsProp.GetArrayElementAtIndex(i).stringValue, textAreaStyle , GUILayout.Height(80));
            }

            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();
        }

        serializedObject.ApplyModifiedProperties();
    }

}
