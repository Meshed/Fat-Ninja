using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtStompButtonDisable : MonoBehaviour 
{
	public Button AbilityButton;

	private float _disabledTimer = 0.0f;

	void OnEnable()
	{
		PlayerStateListener.StompDisabled += StompButtonDisabled;
	}
	void OnDisable()
	{
		PlayerStateListener.StompDisabled += StompButtonDisabled;
	}

	void Update () 
	{
		if (Time.time < _disabledTimer)
		{
			AbilityButton.enabled = false;
		}
		else if(_disabledTimer != 0.0f && Time.time >= _disabledTimer)
		{
			AbilityButton.enabled = true;
			_disabledTimer = 0.0f;
		}
	}

	private void StompButtonDisabled(float disabledTime)
	{
		_disabledTimer = Time.time + disabledTime;
	}
}
