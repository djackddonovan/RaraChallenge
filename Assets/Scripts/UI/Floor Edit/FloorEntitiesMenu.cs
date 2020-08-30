using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorEntitiesMenu : MonoBehaviour
{

	public FloorEntityButton entityButtonPrefab;

	public Transform content;

	private void OnEnable()
	{
		RebuildEntityList();
	}

	void RebuildEntityList()
	{
		foreach (Transform child in content)
			Destroy(child.gameObject);

		Entity[] entities = Floor.Instance.GetComponentsInChildren<Entity>(true);
		foreach (var entity in entities)
		{
			var button = Instantiate(entityButtonPrefab);
			button.transform.SetParent(content);
			button.Init(entity);
		}
	}

}
