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
	public Image previewImage;

	public GameObject customTag;

	public Button editButton;
	public Button deleteButton;

	public void Init(EntityTemplate _entity, UnityAction<EntityTemplate> _action, bool isDragAction, UnityAction<EntityTemplate> _editAction, UnityAction<EntityTemplate> _deleteAction)
	{
		entity = _entity;

		previewImage.sprite = _entity.preview;
		nameLabel.text = _entity.entityName;

		customTag.SetActive(!entity.isDefaultResource);

		if (_action != null)
		{
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.callback.AddListener((data) => _action(entity));
			entry.eventID = isDragAction ? EventTriggerType.BeginDrag : EventTriggerType.PointerClick;
			mainButton.triggers.Add(entry);
		}

		if (_editAction == null)
		{
			editButton.gameObject.SetActive(false);
		}
		else
		{
			editButton.gameObject.SetActive(true);
			editButton.onClick.AddListener(() => _editAction(entity));
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
