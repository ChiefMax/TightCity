using UnityEngine;
using UnityEditor;

namespace Demo {
	[CustomEditor(typeof(GeneratedObjectControl))]
	public class BuildingPainterEditor : Editor {
		public override void OnInspectorGUI() {

            base.OnInspectorGUI();
            GeneratedObjectControl generate = (GeneratedObjectControl)target;
            if (GUILayout.Button("Generate"))
            {
                generate.Generate();
            }
        }
	}
}
