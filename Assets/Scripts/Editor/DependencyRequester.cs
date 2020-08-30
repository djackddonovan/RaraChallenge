using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class DependencyRequester : EditorWindow
{

	Object asset = null;

	bool recursive = true;

	[MenuItem("Window/Dependency Requester")]
	static void Init()
	{
		DependencyRequester window = GetWindow< DependencyRequester>();
		window.Show();
	}

	void OnGUI()
	{
		asset = EditorGUILayout.ObjectField("Asset", asset, typeof(Object), false);

		recursive = EditorGUILayout.Toggle("Recursive", recursive);
	
		if (GUILayout.Button("Request Dependencies"))
		{
			if (asset != null)
			{
				string assetPath = AssetDatabase.GetAssetPath(asset);
				List<string> dependencies = AssetDatabase.GetDependencies(assetPath).ToList();
				dependencies.Sort();

				string completeDependencies = "Dependencies (" + assetPath + "):";
				if (dependencies.Count > 0)
				{
					foreach (string dependency in dependencies)
						completeDependencies += "\n" + dependency;
				}
				else
				{
					completeDependencies += "\nNONE";
				}
				
				Debug.Log(completeDependencies);
			}
		}
	}

}
