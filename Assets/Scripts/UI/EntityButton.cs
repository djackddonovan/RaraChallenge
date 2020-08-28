using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EntityButton : MonoBehaviour
{

	EntityTemplate entity;

	public Text nameLabel;
	public RawImage previewImage;

	public GameObject customTag;

	public void Init(EntityTemplate _entity, UnityAction<EntityTemplate> _action = null)
	{
		entity = _entity;

		previewImage.texture = _entity.preview;
		nameLabel.text = _entity.entityName;

		customTag.SetActive(!entity.isDefaultResource);

		if (_action != null)
		{
			//GetComponentInChildren<Button>().onClick.AddListener(() => _action(entity));
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.callback.AddListener((data) => _action(entity));
			entry.eventID = EventTriggerType.BeginDrag;
			GetComponentInChildren<EventTrigger>().triggers.Add(entry);
		}
	}

}
