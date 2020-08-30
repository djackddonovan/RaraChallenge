using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayData : Singleton<PlayData>
{

	public int Score { get; private set; }


	public void Initialize()
	{
		Score = 0;
	}

	public void AddScore(int _value)
	{
		Score += _value;
	}

}
