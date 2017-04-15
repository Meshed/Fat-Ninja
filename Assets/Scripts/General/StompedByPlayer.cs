using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompedByPlayer : MonoBehaviour 
{
	public delegate void StompedByPlayerHandler();
	public event StompedByPlayerHandler HitByPlayer;

	void OnTriggerEnter2D(Collider2D collidedObject)
	{
		if (collidedObject.tag == "Player")
		{
			if (HitByPlayer != null)
			{
				HitByPlayer ();
			}
		}
	}
}
