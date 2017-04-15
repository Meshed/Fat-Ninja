using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Controller : MonoBehaviour 
{
	public TakeDamageFromPlayerWeapon weaponColliderListener;

	void OnEnable()
	{
		weaponColliderListener.HitByWeapon += HitByPlayerWeapon;
	}
	void OnDisable()
	{
		weaponColliderListener.HitByWeapon -= HitByPlayerWeapon;
	}

	private void HitByPlayerWeapon()
	{
		Destroy (gameObject);
	}
}
