using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EntityList : MonoBehaviour
{

	public class BuildParams
	{
		public UnityAction<EntityTemplate> entityButtonAction = null;
		public bool showCustomEntities = false;
		public UnityAction newEntityButtonAction = null;
		public UnityAction<EntityTemplate> editAction = null;
		public UnityAction<EntityTemplate> deleteAction = null;
		public bool showExtraButtonsOnDefaultEntities = false;
	}

	public EntityButton buttonPrefab;
	public Button newEntityButtonPrefab;
	public GameObject separatorPrefab;

	public Transform listObject;

	public bool buttonAreDragAndDrop = false;

	public void Build(BuildParams _params)
	{
		foreach (Transform go in listObject)
			Destroy(go.gameObject);

		foreach (var entity in EditorGlobals.Instance.defaultEntities)
		{
			var button = Instantiate(buttonPrefab);
			if (_params.showExtraButtonsOnDefaultEntities)
				button.Init(entity, _params.entityButtonAction, buttonAreDragAndDrop, _params.editAction, _params.deleteAction);
			else
				button.Init(entity, _params.entityButtonAction, buttonAreDragAndDrop, null, null);
			button.transform.SetParent(listObject);
		}

		if (_params.showCustomEntities)
		{
			var separator = Instantiate(separatorPrefab);
			separator.transform.SetParent(listObject);

			foreach (var entity in GameData.gameEntities)
			{
				var button = Instantiate(buttonPrefab);
				button.Init(entity, _params.entityButtonAction, buttonAreDragAndDrop, _params.editAction, _params.deleteAction);
				button.transform.SetParent(listObject);
			}
		}

		if (_params.newEntityButtonAction != null)
		{
			var separator = Instantiate(separatorPrefab);
			separator.transform.SetParent(listObject);

			var newEntityButton = Instantiate(newEntityButtonPrefab);
			newEntityButton.transform.SetParent(listObject);
			newEntityButton.onClick.AddListener(_params.newEntityButtonAction);
		}
	}

}
