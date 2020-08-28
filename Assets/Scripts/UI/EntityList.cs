using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class EntityList : MonoBehaviour
{

	public EntityTemplate[] defaultEntities;

	public EntityButton buttonPrefab;
	public Button newEntityButtonPrefab;
	public GameObject separatorPrefab;

	public Transform listObject;

	private void OnEnable()
	{
		Rebuild();
	}

	public void Rebuild()
	{
		foreach (Transform go in listObject)
			Destroy(go.gameObject);

		foreach (var entity in defaultEntities)
		{
			var button = Instantiate(buttonPrefab);
			button.Init(entity, SpawnEntity);
			button.transform.SetParent(listObject);
		}

		var separator = Instantiate(separatorPrefab);
		separator.transform.SetParent(listObject);

		foreach (var entity in GameData.gameEntities)
		{
			var button = Instantiate(buttonPrefab);
			button.Init(entity, SpawnEntity);
			button.transform.SetParent(listObject);
		}

		separator = Instantiate(separatorPrefab);
		separator.transform.SetParent(listObject);

		var newEntityButton = Instantiate(newEntityButtonPrefab);
		newEntityButton.transform.SetParent(listObject);
		newEntityButton.onClick.AddListener(CreateNewEntity);
	}

	void SpawnEntity(EntityTemplate _entity)
	{
		Entity newEntity = _entity.Spawn();
		FloorEditor.EditEntityPosition(newEntity);
	}

	void CreateNewEntity()
	{

	}

}
