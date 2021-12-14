using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif




/**
 * Button that return to normal mode when pointer was pressed and has exited. 
*/
public class CorrectedButton : Button {
	public UnityEvent buttonDown = new UnityEvent();
	public UnityEvent buttonUp = new UnityEvent();

	public override void OnPointerExit(PointerEventData eventData) {
		base.OnPointerExit(eventData);

		buttonUp.Invoke();
		DoStateTransition(SelectionState.Normal, true);
	}

	public override void OnPointerEnter(PointerEventData eventData) {
		base.OnPointerEnter(eventData);

		buttonDown.Invoke();
	}
}

#if UNITY_EDITOR
[CustomEditor(typeof(CorrectedButton))]
public class CorrectedButtonEditor : UnityEditor.UI.ButtonEditor {
	public override void OnInspectorGUI() {
		base.OnInspectorGUI();

		EditorGUILayout.PropertyField(serializedObject.FindProperty("buttonDown"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("buttonUp"));
		serializedObject.ApplyModifiedProperties();
	}
}
#endif