using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum FloorEditorState
{
	Default,
	EditEntityPosition
}

public class FloorEditor : Singleton<FloorEditor>
{

	FloorEditorState state = FloorEditorState.Default;

	Entity selectedEntity;

	public static void EditEntityPosition(Entity _entity)
	{
		Instance.EditEntityPositionInternal(_entity);
	}

	void EditEntityPositionInternal(Entity _entity)
	{
		MenuManager.Hide();
		selectedEntity = _entity;
		state = FloorEditorState.EditEntityPosition;
		UpdateEditEntityPosition();
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
			state = FloorEditorState.Default;
			MenuManager.Show();
		}
	}

}
