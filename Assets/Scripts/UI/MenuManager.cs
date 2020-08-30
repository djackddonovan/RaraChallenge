using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : Singleton<MenuManager>
{

	public enum MenuType
	{
		FloorEditMenu,
		EntityEditMenu,
		InGameMenu
	}

	FloorEditMenu floorEditMenu;
	EntityEditMenu entityEditMenu;
	InGameMenu inGameMenu;

	public static void OpenFloorEditMenu(FloorEditMenu.SubMenu subMenu = FloorEditMenu.SubMenu.AddEntitiesMenu)
	{
		Instance.InnerOpenMenu(MenuType.FloorEditMenu);
		Instance.floorEditMenu.SetCurrentMenu(subMenu);
	}

	public static void OpenEntityEditMenu(EntityTemplate _editedEntity)
	{
		Instance.InnerOpenMenu(MenuType.EntityEditMenu);
		Instance.entityEditMenu.StartEdit(_editedEntity);
	}

	public static void OpenInGameMenu()
	{
		Instance.InnerOpenMenu(MenuType.InGameMenu);
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
		inGameMenu = GetComponentInChildren<InGameMenu>(true);

		OpenFloorEditMenu(FloorEditMenu.SubMenu.AddEntitiesMenu);
	}

	void InnerOpenMenu(MenuType _menuType)
	{
		Show();

		floorEditMenu.gameObject.SetActive(_menuType == MenuType.FloorEditMenu);
		entityEditMenu.gameObject.SetActive(_menuType == MenuType.EntityEditMenu);
		inGameMenu.gameObject.SetActive(_menuType == MenuType.InGameMenu);
	}

}
