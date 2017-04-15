using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D collidedObject)
	{
		switch (collidedObject.tag) 
		{
			case "Player":
				collidedObject.SendMessage ("HitDeathTrigger", SendMessageOptions.DontRequireReceiver);
				break;
		}
	}
}
