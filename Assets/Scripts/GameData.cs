using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{

	public static List<EntityTemplate> gameEntities = new List<EntityTemplate>();

	public static void DeleteEntityTemplate(EntityTemplate _template)
	{
		gameEntities.Remove(_template);
		ScriptableObject.Destroy(_template);
	}

}
