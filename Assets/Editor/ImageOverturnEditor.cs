using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ImageOverturn))]
public class ImageOverturnEditor : ImageEditor
{
	public new ImageOverturn target;

	private SerializedProperty _spFlipHor;
	private SerializedProperty _spFlipVer;
	private GUIContent _gcFlipHor;
	private GUIContent _gcFlipVer;

	protected override void OnEnable()
	{
		base.OnEnable();

		target = base.target as ImageOverturn;

		_spFlipHor = serializedObject.FindProperty("flipHor");
		_spFlipVer = serializedObject.FindProperty("flipVer");
		_gcFlipHor = EditorGUIUtility.TrTextContent("水平翻转", null);
		_gcFlipVer = EditorGUIUtility.TrTextContent("垂直翻转", null);
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		EditorGUILayout.PropertyField(_spFlipHor, _gcFlipHor);
		EditorGUILayout.PropertyField(_spFlipVer, _gcFlipVer);

		if (GUI.changed)
		{
			EditorUtility.SetDirty(target);
		}
		serializedObject.ApplyModifiedProperties();
	}
}
