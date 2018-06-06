using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GlobalValues))]
public class GlobalValuesEditor : Editor{

    private SerializedProperty defaultValuesProp;
    int valueEnumSize;

    void OnEnable()
    {
        defaultValuesProp = serializedObject.FindProperty("defaultValues");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        /*if (!defaultValuesProp.isArray) {
			throw new UnityException ("defaultValuesProp is not an Array. Correct that.");
		}*/
        valueEnumSize = System.Enum.GetNames(typeof(Resource)).Length;
        defaultValuesProp.arraySize = valueEnumSize;

        for (int i = 0; i < defaultValuesProp.arraySize; i++)
        {
            defaultValuesProp.GetArrayElementAtIndex(i).intValue = EditorGUILayout.IntField(System.Enum.GetNames(typeof(Resource))[i], defaultValuesProp.GetArrayElementAtIndex(i).intValue);
        }

        serializedObject.ApplyModifiedProperties();
    }

}
