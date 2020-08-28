using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Entity : MonoBehaviour
{

	void OnMouseDown()
	{
		if (EventSystem.current.IsPointerOverGameObject())
			return;

		// if (game running)
		//	TriggetBehaviourClicks()
		// else
			FloorEditor.EditEntityPosition(this);
	}

	void TriggetBehaviourClicks()
	{

	}

}
