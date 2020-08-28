using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : Singleton<MenuManager>
{

	public static void Hide()
	{
		Instance.gameObject.SetActive(false);
	}

	public static void Show()
	{
		Instance.gameObject.SetActive(true);
	}

}
