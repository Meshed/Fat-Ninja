using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowWeapon : MonoBehaviour 
{
	public float ThrowSpeed = 3.0f;
	public float SelfDestructDelay = 1.5f;

	private float _selfDestructTimer = 0.0f;

	void Start()
	{
		_selfDestructTimer = Time.time + SelfDestructDelay;
	}

	// Update is called once per frame
	void Update () 
	{
		if (Time.time >= _selfDestructTimer)
		{
			DestroyObject (gameObject);
		}
		else
		{
			Vector2 weaponForce = new Vector2 (ThrowSpeed, 0.0f);

			Rigidbody2D rigidBody = GetComponent<Rigidbody2D> ();

			rigidBody.velocity = weaponForce;
		}
	}
}
