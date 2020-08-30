using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorEditMenu : MonoBehaviour
{

	public enum SubMenu : int
	{
		AddEntitiesMenu,
		FloorEntitiesMenu,
		Game
	}

	[Header("Main")]
	public GameObject content;
	public Button maximizeButton;
	public Button minimizeButton;
	public Dropdown menuDropdown;

	[Header("Sub")]
	public GameObject addEntitiesMenu;
	public GameObject floorEntitiesMenu;
	public GameObject gameMenu;

	[Header("Game Menu")]
	public Button playButton;

	private void Awake()
	{
		menuDropdown.onValueChanged.AddListener(SetCurrentMenu);
		maximizeButton.onClick.AddListener(Maximize);
		minimizeButton.onClick.AddListener(Minimize);
		playButton.onClick.AddListener(Play);
	}

	void OnEnable()
	{
		menuDropdown.SetValueWithoutNotify(0);

		// add any created entity to the list
		RebuildEntityList();
	}

	void OnDisable()
	{
		if (FloorEditor.Instance)
			FloorEditor.Instance.Deselect();
	}

	public void SetCurrentMenu(SubMenu menu)
	{
		addEntitiesMenu.SetActive(menu == SubMenu.AddEntitiesMenu);
		floorEntitiesMenu.SetActive(menu == SubMenu.FloorEntitiesMenu);
		gameMenu.SetActive(menu == SubMenu.Game);

		menuDropdown.SetValueWithoutNotify((int)menu);
	}

	void SetCurrentMenu(int idx)
	{
		SetCurrentMenu((SubMenu)idx);
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
		EntityList.BuildParams buildParams = new EntityList.BuildParams();
		buildParams.entityButtonAction = SpawnEntity;
		buildParams.showCustomEntities = true;
		buildParams.newEntityButtonAction = CreateNewEntity;
		buildParams.editAction = EditEntity;
		buildParams.deleteAction = DeleteEntityTemplate;

		EntityList entityList = addEntitiesMenu.GetComponentInChildren<EntityList>(true);
		entityList.Build(buildParams);
	}

	void SpawnEntity(EntityTemplate _entity)
	{
		Entity newEntity = _entity.Spawn();
		FloorEditor.Instance.EditEntityPosition(newEntity);
	}

	void CreateNewEntity()
	{
		EntityTemplate newEntity = ScriptableObject.CreateInstance<EntityTemplate>();
		GameData.gameEntities.Add(newEntity);
		MenuManager.OpenEntityEditMenu(newEntity);
	}

	void EditEntity(EntityTemplate _entity)
	{
		MenuManager.OpenEntityEditMenu(_entity);
	}

	void DeleteEntityTemplate(EntityTemplate _entity)
	{
		if (Floor.Instance.HasInstancesOfEntity(_entity))
			MessageMenu.Show("Delete all instances of the entity from the floor before deleting it.");
		else
			GameData.DeleteEntityTemplate(_entity);

		RebuildEntityList();
	}

	void Play()
	{
		GameManager.Instance.StartPlayMode();
	}

}
