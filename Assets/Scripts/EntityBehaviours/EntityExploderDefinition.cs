using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityExploderDefinition : EntityBehaviourDefinition
{

	public override void OnEntityClick(Entity _entity)
	{
		GameObject explosionEffect = Object.Instantiate(GameManager.Instance.globalData.explosionPrefab);
		explosionEffect.transform.position = _entity.transform.position;

		Object.Destroy(_entity.gameObject);
	}

}
