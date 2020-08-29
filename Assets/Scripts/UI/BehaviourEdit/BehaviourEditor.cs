using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public abstract class BehaviourEditor : MonoBehaviour
{

	public Button deleteButton;

	abstract public Type BehaviourType { get; }
	abstract public string BehaviourName { get; }

	EntityBehaviourDefinition target;
	UnityAction<EntityBehaviourDefinition> onDestroy;

	protected virtual void Awake()
	{
		deleteButton.onClick.AddListener(Delete);
	}

	public virtual void Init(EntityBehaviourDefinition _target, UnityAction<EntityBehaviourDefinition> _onDestroy)
	{
		target = _target;
		onDestroy = _onDestroy;
	}

	protected void Delete()
	{
		Destroy(gameObject);
		onDestroy.Invoke(target);
	}

}
