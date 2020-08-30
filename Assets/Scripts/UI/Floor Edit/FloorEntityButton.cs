using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorEntityButton : MonoBehaviour
{

	public Button mainButton;
	public Button deleteButton;
	public Text entityName;
	public Image entityPreview;

	Entity targetEntity;

	void Start()
	{
		mainButton.onClick.AddListener(SelectEntity);
		deleteButton.onClick.AddListener(DestroyEntity);
	}

	public void Init(Entity _targetEntity)
	{
		targetEntity = _targetEntity;

		entityName.text = targetEntity.template.entityName;
		entityPreview.sprite = targetEntity.template.preview;
	}

	void SelectEntity()
	{
		FloorEditor.Instance.Select(targetEntity);
	}

	void DestroyEntity()
	{
		if (FloorEditor.Instance.CurrentSelection == targetEntity)
			FloorEditor.Instance.Deselect();

		Destroy(targetEntity.gameObject);
		Destroy(gameObject);
	}

}
