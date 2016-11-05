using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.IO;
using System.Collections.Generic;
//[CustomEditor(typeof(Enemies_Manager))]
public class WaveDataEditor : Editor {
    private List<ReorderableList> Wavelist;
    private void OnEnable() {
        Wavelist.Add(new ReorderableList(serializedObject,
                                       serializedObject.FindProperty("Waves").FindPropertyRelative("Enemies"),
                                       true, true, true, true));
        //Wavelist[0].drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
        //    var element = Wavelist[0].serializedProperty.GetArrayElementAtIndex(index);
        //    rect.y += 2;
        //    EditorGUI.PropertyField(new Rect(rect.x, rect.y, 60, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("Type"), GUIContent.none);
        //    EditorGUI.PropertyField(new Rect(rect.x + 60, rect.y, rect.width - 60 - 30, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("Prefab"), GUIContent.none);
        //    EditorGUI.PropertyField(new Rect(rect.x + rect.width - 30, rect.y, 30, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("Count"), GUIContent.none);
        //};
    }
	
	public override void OnInspectorGUI() {
        DrawDefaultInspector();
        serializedObject.Update();
        Wavelist[0].DoLayoutList();
        serializedObject.ApplyModifiedProperties();
	}

	private void clickHandler(object target) {
		
		serializedObject.ApplyModifiedProperties();
	}
	
	private struct WaveCreationParams {
		//public Wave.WaveType Type;
		public string Path;
	}
}