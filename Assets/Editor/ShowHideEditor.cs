using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ShowHide))]
[CanEditMultipleObjects]
public class ShowHideEditor : Editor
{
	SerializedProperty transitionType;
	SerializedProperty showFadeValue;
	SerializedProperty hideFadeValue;
	SerializedProperty showPosition;
	SerializedProperty hidePosition;
	SerializedProperty time;
	SerializedProperty easeType;
	SerializedProperty disableAfterHide;

	private void OnEnable()
	{
		transitionType = serializedObject.FindProperty("transitionType");
		showFadeValue = serializedObject.FindProperty("showFadeValue");
		hideFadeValue = serializedObject.FindProperty("hideFadeValue");
		showPosition = serializedObject.FindProperty("showPosition");
		hidePosition = serializedObject.FindProperty("hidePosition");
		time = serializedObject.FindProperty("time");
		easeType = serializedObject.FindProperty("easeType");
		disableAfterHide = serializedObject.FindProperty("disableAfterHide");
	}
	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUILayout.PropertyField(transitionType);		

		switch (transitionType.enumValueIndex)
		{
			case (int)ShowHide.Transition.Move:
				EditorGUILayout.PropertyField(showPosition);
				EditorGUILayout.PropertyField(hidePosition);
				break;
			case (int)ShowHide.Transition.Fade:
				EditorGUILayout.PropertyField(showFadeValue);
				EditorGUILayout.PropertyField(hideFadeValue);
				break;
			default:
				break;
		}

		EditorGUILayout.PropertyField(time);
		EditorGUILayout.PropertyField(easeType);
		EditorGUILayout.PropertyField(disableAfterHide);

		serializedObject.ApplyModifiedProperties();
	}
}
