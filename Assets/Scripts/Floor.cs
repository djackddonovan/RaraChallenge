using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : Singleton<Floor>
{

	public bool HasInstancesOfEntity(EntityTemplate _template)
	{
		Entity[] entities = GetComponentsInChildren<Entity>();

		foreach (var entity in entities)
		{
			if (entity.template == _template)
				return true;
		}

		return false;
	}

}
