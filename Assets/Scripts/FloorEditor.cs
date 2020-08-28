using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

enum FloorEditorState
{
	Default,
	EditEntityPosition
}

public class FloorEditor : Singleton<FloorEditor>
{

	public EventTrigger deleteEntityButton;

	FloorEditorState state = FloorEditorState.Default;

	Entity selectedEntity;

	public static void EditEntityPosition(Entity _entity)
	{
		Instance.EditEntityPositionInternal(_entity);
	}

	private void Awake()
	{
		EventTrigger.Entry exitDeleteButton = new EventTrigger.Entry();
		exitDeleteButton.callback.AddListener((data) => ReleaseOnDeleteButton());
		exitDeleteButton.eventID = EventTriggerType.PointerExit;
		deleteEntityButton.triggers.Add(exitDeleteButton);
	}

	void EditEntityPositionInternal(Entity _entity)
	{
		MenuManager.Hide();
		selectedEntity = _entity;
		state = FloorEditorState.EditEntityPosition;
		// snap the entity directly to the pointer
		UpdateEditEntityPosition();
		deleteEntityButton.gameObject.SetActive(true);
	}

	void Update()
	{
		switch(state)
		{
		case FloorEditorState.EditEntityPosition:
			UpdateEditEntityPosition();
			break;
		default:
			break;
		}
	}

	void UpdateEditEntityPosition()
	{
		Ray cursorRay = Camera.main.ScreenPointToRay(Input.mousePosition);

		Vector3 floorPoint = cursorRay.origin - (cursorRay.origin.y / cursorRay.direction.y) * cursorRay.direction;

		selectedEntity.transform.position = floorPoint;

		if (Input.GetMouseButtonUp(0))
		{
			// Drop and stop moving the entity
			state = FloorEditorState.Default;
			selectedEntity = null;
			MenuManager.Show();
			deleteEntityButton.gameObject.SetActive(false);
		}
	}

	void ReleaseOnDeleteButton()
	{
		if (Input.GetMouseButton(0)) // cursor left the button, wasn't released on it
			return;

		if (state == FloorEditorState.EditEntityPosition &&
			selectedEntity)
		{
			Destroy(selectedEntity.gameObject);
		}
	}

}
