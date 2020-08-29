using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : Singleton<MenuManager>
{

	public enum MenuType
	{
		FloorEditMenu,
		EntityEditMenu
	}

	FloorEditMenu floorEditMenu;
	EntityEditMenu entityEditMenu;

	public static void OpenFloorEditMenu()
	{
		Instance.InnerOpenMenu(MenuType.FloorEditMenu);
	}

	public static void OpenEntityEditMenu(EntityTemplate _editedEntity)
	{
		Instance.InnerOpenMenu(MenuType.EntityEditMenu);
		Instance.entityEditMenu.StartEdit(_editedEntity);
	}

	public static void Hide()
	{
		Instance.gameObject.SetActive(false);
	}

	public static void Show()
	{
		Instance.gameObject.SetActive(true);
	}

	private void Awake()
	{
		floorEditMenu = GetComponentInChildren<FloorEditMenu>(true);
		entityEditMenu = GetComponentInChildren<EntityEditMenu>(true);

		OpenFloorEditMenu();
	}

	void InnerOpenMenu(MenuType _menuType)
	{
		Show();

		floorEditMenu.gameObject.SetActive(_menuType == MenuType.FloorEditMenu);
		entityEditMenu.gameObject.SetActive(_menuType == MenuType.EntityEditMenu);
	}

}
