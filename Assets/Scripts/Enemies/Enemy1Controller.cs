using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Controller : MonoBehaviour 
{
	public StompedByPlayer stompedByPlayer;

	void OnEnable()
	{
		stompedByPlayer.HitByPlayer += HitByPlayer;
	}
	void OnDisable()
	{
		stompedByPlayer.HitByPlayer -= HitByPlayer;
	}

	private void HitByPlayer()
	{
		Destroy (gameObject);
	}
}
