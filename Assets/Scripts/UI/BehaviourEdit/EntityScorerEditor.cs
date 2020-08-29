using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EntityScorerEditor : BehaviourEditor
{

	public override Type BehaviourType { get { return typeof(EntityScorerDefinition); } }
	public override string BehaviourName { get { return "Scorer"; } }

	public InputField scoreAmountField;

	EntityScorerDefinition targetScorer;

	protected override void Awake()
	{
		base.Awake();
		scoreAmountField.onValueChanged.AddListener(SetScore);
	}

	public override void Init(EntityBehaviourDefinition _target, UnityAction<EntityBehaviourDefinition> _onDestroy)
	{
		base.Init(_target, _onDestroy);

		targetScorer = _target as EntityScorerDefinition;
		scoreAmountField.SetTextWithoutNotify(targetScorer.score.ToString());
	}

	void SetScore(string _value)
	{
		targetScorer.score = Mathf.Clamp(int.Parse(_value), 0, 99999);
		scoreAmountField.SetTextWithoutNotify(targetScorer.score.ToString());
	}

}
