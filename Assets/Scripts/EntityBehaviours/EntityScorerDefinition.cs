using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityScorerDefinition : EntityBehaviourDefinition
{

	public int score = 1;

	public override void OnEntityClick(Entity _entity)
	{
		PlayData.Instance.AddScore(score);
	}
}
