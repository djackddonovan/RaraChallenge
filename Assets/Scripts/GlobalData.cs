using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GlobalData : ScriptableObject
{

	public GameObject explosionPrefab;

#if UNITY_EDITOR
	[MenuItem("Assets/Create/Global Data")]
	public static void CreateGlobalDataAsset()
	{
		GlobalData asset = ScriptableObject.CreateInstance<GlobalData>();

		AssetDatabase.CreateAsset(asset, "Assets/Global Data.asset");
		AssetDatabase.SaveAssets();

		EditorUtility.FocusProjectWindow();

		Selection.activeObject = asset;
	}
#endif

}
