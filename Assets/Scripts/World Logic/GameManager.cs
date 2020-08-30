using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

	Floor editorFloor;
	GameObject floorCopy;

	public bool PlayModeActive { get; private set; } = false;

	public void StartPlayMode()
	{
		if (PlayModeActive)
		{
			Debug.LogWarning("Play Mode already active");
			return;
		}

		PlayModeActive = true;

		PlayData.Instance.Initialize();
		MenuManager.OpenInGameMenu();

		editorFloor = Floor.Instance;
		floorCopy = Instantiate(Floor.Instance.gameObject);
		floorCopy.name = "Floor (Game Copy)";
		editorFloor.gameObject.SetActive(false);
	}

	public void EndPlayMode()
	{
		if (!PlayModeActive)
		{
			Debug.LogWarning("Play Mode already inactive");
			return;
		}

		PlayModeActive = false;

		MenuManager.OpenFloorEditMenu(FloorEditMenu.SubMenu.Game);

		Destroy(floorCopy);
		editorFloor.gameObject.SetActive(true);
	}

}
