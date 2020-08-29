using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EntityEditMenu : MonoBehaviour
{

	public GameObject rootEntityMenu;
	public GameObject behaviourEditMenu;

	[Header("Main")]
	public InputField entityNameField;
	public Image entityPreview;
	public Button confirmButton;

	EntityTemplate editedEntity;

	[Header("Behaviours")]
	public BehaviourEditor[] behaviourEditorPrefabs;

	public Dropdown newBehaviourDropdown;
	List<Type> newBehaviourTypes;

	private void Awake()
	{
		entityNameField.onValueChanged.AddListener(SetEntityName);
		confirmButton.onClick.AddListener(ValidateEntity);
		newBehaviourDropdown.onValueChanged.AddListener(AddBehaviour);
	}

	public void StartEdit(EntityTemplate _editedEntity)
	{
		editedEntity = _editedEntity;

		var oldBehaviourEditors = newBehaviourDropdown.transform.parent.GetComponentsInChildren<BehaviourEditor>();
		foreach (var editor in oldBehaviourEditors)
			Destroy(editor.gameObject);

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

			foreach (var behaviour in editedEntity.behaviours)
			{
				CreateBehaviourEditor(behaviour);
				newBehaviourDropdown.transform.SetAsLastSibling();
				UpdateBehaviourDropdown();
			}

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
		entityPreview.sprite = editedEntity.preview;
		UpdateBehaviourDropdown();
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

	void UpdateBehaviourDropdown()
	{
		List<Dropdown.OptionData> behaviourOptions = new List<Dropdown.OptionData>();
		newBehaviourTypes = new List<Type>();

		foreach (BehaviourEditor editor in behaviourEditorPrefabs)
		{
			if (!editedEntity.behaviours.Any((behaviour) => behaviour.GetType() == editor.BehaviourType))
			{
				behaviourOptions.Add(new Dropdown.OptionData(editor.BehaviourName));
				newBehaviourTypes.Add(editor.BehaviourType);
			}
		}

		if (behaviourOptions.Count == 0)
		{
			newBehaviourDropdown.gameObject.SetActive(false);
		}
		else
		{
			newBehaviourDropdown.gameObject.SetActive(true);
			newBehaviourDropdown.options = behaviourOptions;

			// Hack: Dropdown.onValueChanged doesn't trigger when selecting an item with the same index as the
			// last selected one, so we create a temp one, select it, and delete it
			newBehaviourDropdown.options.Add(new Dropdown.OptionData("TEMP"));
			newBehaviourDropdown.value = newBehaviourDropdown.options.Count - 1;
			newBehaviourDropdown.options.RemoveAt(newBehaviourDropdown.options.Count - 1);
		}
	}

	void AddBehaviour(int idx)
	{
		if (idx >= newBehaviourTypes.Count)
			return;

		Type type = newBehaviourTypes[idx];

		EntityBehaviourDefinition newBehaviour = (EntityBehaviourDefinition)Activator.CreateInstance(type);
		editedEntity.behaviours.Add(newBehaviour);
		
		CreateBehaviourEditor(newBehaviour);
		newBehaviourDropdown.transform.SetAsLastSibling();
		UpdateBehaviourDropdown();
	}

	void CreateBehaviourEditor(EntityBehaviourDefinition _behaviour)
	{
		Type type = _behaviour.GetType();

		BehaviourEditor editorPrefab = null;
		foreach (var p in behaviourEditorPrefabs)
		{
			if (p.BehaviourType == type)
			{
				editorPrefab = p;
				break;
			}
		}

		if (editorPrefab)
		{
			var editor = Instantiate(editorPrefab);
			editor.transform.SetParent(newBehaviourDropdown.transform.parent);
			editor.Init(_behaviour, DeleteBehaviour);
		}
		else
		{
			Debug.LogError("Missing Behaviour Editor");
		}
	}

	void DeleteBehaviour(EntityBehaviourDefinition _behaviour)
	{
		editedEntity.behaviours.Remove(_behaviour);
		UpdateBehaviourDropdown();
	}

}
