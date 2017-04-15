using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageFromPlayerWeapon : MonoBehaviour 
{
	public delegate void HitByPlayerWeapon();
	public event HitByPlayerWeapon HitByWeapon;

	void OnTriggerEnter2D(Collider2D collidedObject)
	{
		if (collidedObject.tag == "Weapon")
		{
			if (HitByWeapon != null)
			{
				HitByWeapon ();
			}
		}
	}
}
