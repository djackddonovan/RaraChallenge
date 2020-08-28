using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

// All the info related to a saved entity
public class EntityTemplate : ScriptableObject
{

	public bool isDefaultResource;

	public Texture2D preview;
	public string entityName;
	public Entity prefab;

#if UNITY_EDITOR
	[MenuItem("Assets/Create/Entity Template")]
	public static void CreateMyAsset()
	{
		EntityTemplate asset = ScriptableObject.CreateInstance<EntityTemplate>();

		AssetDatabase.CreateAsset(asset, "Assets/Entity Template.asset");
		AssetDatabase.SaveAssets();

		EditorUtility.FocusProjectWindow();

		Selection.activeObject = asset;
	}
#endif

	public Entity Spawn()
	{
		Entity obj = Instantiate(prefab);
		obj.transform.SetParent(Floor.Instance.transform);
		return obj;
	}

}
