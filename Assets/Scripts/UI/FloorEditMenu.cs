using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorEditMenu : MonoBehaviour
{
	[Header("Main")]
	public GameObject content;
	public Button maximizeButton;
	public Button minimizeButton;
	public Dropdown menuDropdown;

	[Header("Sub")]
	public GameObject addEntitiesMenu;
	public GameObject floorEntitiesMenu;
	public GameObject gameMenu;

	private void Awake()
	{
		menuDropdown.onValueChanged.AddListener(SetCurrentMenu);
		maximizeButton.onClick.AddListener(Maximize);
		minimizeButton.onClick.AddListener(Minimize);
	}

	void OnEnable()
	{
		menuDropdown.SetValueWithoutNotify(0);
		SetCurrentMenu(0);

		// add any created entity to the list
		RebuildEntityList();
	}

	void SetCurrentMenu(int idx)
	{
		addEntitiesMenu.SetActive(idx == 0);
		floorEntitiesMenu.SetActive(idx == 1);
		gameMenu.SetActive(idx == 2);
	}

	public void Maximize()
	{
		content.SetActive(true);
		maximizeButton.gameObject.SetActive(false);
	}

	public void Minimize()
	{
		content.SetActive(false);
		maximizeButton.gameObject.SetActive(true);
	}

	void RebuildEntityList()
	{
		EntityList entityList = GetComponentInChildren<EntityList>();
		entityList.Build(SpawnEntity, true, CreateNewEntity);
	}

	void SpawnEntity(EntityTemplate _entity)
	{
		Entity newEntity = _entity.Spawn();
		FloorEditor.Instance.EditEntityPosition(newEntity);
	}

	void CreateNewEntity()
	{
		FloorEditor.Instance.Deselect();

		EntityTemplate newEntity = ScriptableObject.CreateInstance<EntityTemplate>();
		GameData.gameEntities.Add(newEntity);
		MenuManager.OpenEntityEditMenu(newEntity);
	}

}
