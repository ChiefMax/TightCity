using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

/**
 * This is a sample editor window that shows how to generate stuff at editor time.
 * 
 * @author J.C. Wichman / InnerDriveStudios.com 
 */

 [CustomEditor(typeof(SampleScript))]
public class SampleScriptEditor : Editor
{

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		SampleScript sampleScript = target as SampleScript;
	
		if (GUILayout.Button("Generate"))
		{
			if (sampleScript.prefabToPlace != null)
			{
				Undo.SetCurrentGroupName("Auto create objects");
				int group = Undo.GetCurrentGroup();

				for (int i = 0; i < sampleScript.amountOfObjectsToPlace; i++)
				{
					GameObject go = Instantiate(sampleScript.prefabToPlace, Random.onUnitSphere * 4, Quaternion.identity);

					Undo.RegisterCreatedObjectUndo(go, "Undo gameobject create");
				}

				Undo.CollapseUndoOperations(group);

				//important to save the changes
				EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
			}
		}
	}
}
