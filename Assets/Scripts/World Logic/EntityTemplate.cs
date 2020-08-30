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

	public Sprite preview;
	public string entityName;
	public Entity prefab;

	public List<EntityBehaviourDefinition> behaviours = new List<EntityBehaviourDefinition>();

#if UNITY_EDITOR
	[MenuItem("Assets/Create/Entity Template")]
	public static void CreateEntityTemplateAsset()
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
		Entity entity = Instantiate(prefab);
		entity.transform.SetParent(Floor.Instance.transform);
		entity.template = this;
		return entity;
	}

}
