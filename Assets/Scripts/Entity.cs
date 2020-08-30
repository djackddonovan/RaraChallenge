using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Entity : MonoBehaviour
{

	public EntityTemplate template;

	void OnMouseDown()
	{
		if (EventSystem.current.IsPointerOverGameObject())
			return;

		if (GameManager.Instance.PlayModeActive)
			TriggetBehaviourClicks();
		else
			FloorEditor.Instance.EditEntityPosition(this);
	}

	void TriggetBehaviourClicks()
	{
		foreach (var behaviour in template.behaviours)
		{
			behaviour.OnEntityClick(this);
		}
	}

}
