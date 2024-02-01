using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AI_Inspector : EditorWindow {
	[MenuItem("AI/Inspector")]
	static void showWindow() {
		EditorWindow.GetWindow(typeof(AI_Inspector));
	}

	private void OnGUI() {
		GUILayout.BeginHorizontal();
		GUILayout.Label("Actions", EditorStyles.boldLabel);

		if (GUILayout.Button("View Agent")) {
			Camera camera = Camera.main;
			GameObject go = Selection.activeGameObject;
			if (go.TryGetComponent<AI_Agent>(out AI_Agent agent)) {
				camera.transform.parent = agent.transform;
				camera.transform.localPosition = Vector3.back * 5 + Vector3.up * 2;
				camera.transform.localRotation = Quaternion.identity;
			}
		}

		GUILayout.EndHorizontal();

	}
}
