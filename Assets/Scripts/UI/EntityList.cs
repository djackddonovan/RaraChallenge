using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EntityList : MonoBehaviour
{

	public EntityButton buttonPrefab;
	public Button newEntityButtonPrefab;
	public GameObject separatorPrefab;

	public Transform listObject;

	public bool buttonAreDragAndDrop = false;

	public void Build(UnityAction<EntityTemplate> _entityButtonAction, bool _showCustomEntities, UnityAction _newEntityButtonAction)
	{
		foreach (Transform go in listObject)
			Destroy(go.gameObject);

		foreach (var entity in EditorGlobals.Instance.defaultEntities)
		{
			var button = Instantiate(buttonPrefab);
			button.Init(entity, _entityButtonAction, buttonAreDragAndDrop);
			button.transform.SetParent(listObject);
		}

		if (_showCustomEntities)
		{
			var separator = Instantiate(separatorPrefab);
			separator.transform.SetParent(listObject);

			foreach (var entity in GameData.gameEntities)
			{
				var button = Instantiate(buttonPrefab);
				button.Init(entity, _entityButtonAction, buttonAreDragAndDrop);
				button.transform.SetParent(listObject);
			}
		}

		if (_newEntityButtonAction != null)
		{
			var separator = Instantiate(separatorPrefab);
			separator.transform.SetParent(listObject);

			var newEntityButton = Instantiate(newEntityButtonPrefab);
			newEntityButton.transform.SetParent(listObject);
			newEntityButton.onClick.AddListener(_newEntityButtonAction);
		}
	}

}
