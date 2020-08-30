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

	public GameObject selectionIndicator;

	public EventTrigger deleteEntityButton;

	FloorEditorState state = FloorEditorState.Default;

	Entity selectedEntity;

	public void EditEntityPosition(Entity _entity)
	{
		Instance.EditEntityPositionInternal(_entity);
	}

	public void Select(Entity _entity)
	{
		if (!_entity)
			Deselect();
		else
		{
			selectedEntity = _entity;
			selectionIndicator.gameObject.SetActive(true);
		}
	}

	public void Deselect()
	{
		selectedEntity = null;
		if (selectionIndicator)
			selectionIndicator.gameObject.SetActive(false);
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
		Select(_entity);
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

	void LateUpdate()
	{
		if (selectedEntity != null)
		{
			selectionIndicator.transform.position = selectedEntity.transform.position;
		}
	}

	void UpdateEditEntityPosition()
	{
		if (selectedEntity)
		{
			Ray cursorRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			Vector3 floorPoint = cursorRay.origin - (cursorRay.origin.y / cursorRay.direction.y) * cursorRay.direction;
			selectedEntity.transform.position = floorPoint;
		}

		if (!selectedEntity ||
			Input.GetMouseButtonUp(0))
		{
			// Drop and stop moving the entity
			state = FloorEditorState.Default;
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
			Deselect();
		}
	}

}
