using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityEditMenu : MonoBehaviour
{

	[Header("Main")]
	public GameObject rootEntityMenu;
	public GameObject behaviourEditMenu;

	[Header("Behaviour")]
	public InputField entityNameField;
	public RawImage entityPreview;
	public Button confirmButton;

	EntityTemplate editedEntity;

	private void Awake()
	{
		entityNameField.onValueChanged.AddListener(SetEntityName);
		confirmButton.onClick.AddListener(ValidateEntity);
	}

	public void StartEdit(EntityTemplate _editedEntity)
	{
		editedEntity = _editedEntity;

		if (_editedEntity.prefab == null)
		{
			// a new entity is being edited, first show the root menu
			rootEntityMenu.SetActive(true);
			behaviourEditMenu.SetActive(false);

			EntityList.BuildParams buildParams = new EntityList.BuildParams();
			buildParams.entityButtonAction = SelectRootEntity;

			EntityList entityList = rootEntityMenu.GetComponentInChildren<EntityList>();
			entityList.Build(buildParams);
		}
		else
		{
			rootEntityMenu.SetActive(false);
			behaviourEditMenu.SetActive(true);

			InitBehaviourEditMenu();
		}
	}

	void SelectRootEntity(EntityTemplate _root)
	{
		editedEntity.prefab = _root.prefab;
		editedEntity.preview = _root.preview;
		editedEntity.entityName = _root.entityName;

		rootEntityMenu.SetActive(false);
		behaviourEditMenu.SetActive(true);

		InitBehaviourEditMenu();

		entityNameField.Select();
	}

	void InitBehaviourEditMenu()
	{
		entityNameField.SetTextWithoutNotify(editedEntity.entityName);
		entityPreview.texture = editedEntity.preview;
	}

	void SetEntityName(string _newName)
	{
		editedEntity.entityName = _newName;
	}

	void ValidateEntity()
	{
		if (HasDuplicateName())
			MessageMenu.Show("An Entity with that Name already exists");
		else
			MenuManager.OpenFloorEditMenu();
	}

	bool HasDuplicateName()
	{
		foreach (var entity in EditorGlobals.Instance.defaultEntities)
		{
			if (entity.entityName == editedEntity.entityName)
				return true;
		}

		foreach (var entity in GameData.gameEntities)
		{
			if (entity != editedEntity &&
				entity.entityName == editedEntity.entityName)
				return true;
		}

		return false;
	}

}
