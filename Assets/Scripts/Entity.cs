using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Entity : MonoBehaviour
{

	public EntityTemplate template;

	void OnMouseDown()
	{
		if (MouseIsOverUI())
			return;

		if (GameManager.Instance.PlayModeActive)
			TriggetBehaviourClicks();
		else
			FloorEditor.Instance.EditEntityPosition(this);
	}

	static bool MouseIsOverUI()
	{
		PointerEventData rayData = new PointerEventData(EventSystem.current);
		rayData.position = Input.mousePosition;
		List<RaycastResult> res = new List<RaycastResult>();
		EventSystem.current.RaycastAll(rayData, res);
		return res.Count > 0;
	}

	void TriggetBehaviourClicks()
	{
		foreach (var behaviour in template.behaviours)
		{
			behaviour.OnEntityClick(this);
		}
	}

}
