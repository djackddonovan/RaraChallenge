using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EntityButton : MonoBehaviour
{

	EntityTemplate entity;

	public EventTrigger mainButton;

	public Text nameLabel;
	public RawImage previewImage;

	public GameObject customTag;

	public Button deleteButton;

	public void Init(EntityTemplate _entity, UnityAction<EntityTemplate> _action, bool isDragAction, UnityAction<EntityTemplate> _deleteAction = null)
	{
		entity = _entity;

		previewImage.texture = _entity.preview;
		nameLabel.text = _entity.entityName;

		customTag.SetActive(!entity.isDefaultResource);

		if (_action != null)
		{
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.callback.AddListener((data) => _action(entity));
			entry.eventID = isDragAction ? EventTriggerType.BeginDrag : EventTriggerType.PointerClick;
			mainButton.triggers.Add(entry);
		}

		if (_deleteAction == null)
		{
			deleteButton.gameObject.SetActive(false);
		}
		else
		{
			deleteButton.gameObject.SetActive(true);
			deleteButton.onClick.AddListener(() => _deleteAction(entity));
		}
	}

}
