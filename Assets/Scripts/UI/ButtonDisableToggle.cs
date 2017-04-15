using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDisableToggle : MonoBehaviour 
{
	public Button AbilityButton;
	
	private float _disabledTimer = 0.0f;
	
	void OnEnable()
	{
		PlayerStateListener.AttackDisabled += AttackButtonDisabled;
		PlayerStateListener.StompDisabled += StompButtonDisabled;
		PlayerStateListener.TeleportDisabled += TeleportButtonDisabled;
	}
	void OnDisable()
	{
		PlayerStateListener.AttackDisabled -= AttackButtonDisabled;
		PlayerStateListener.StompDisabled += StompButtonDisabled;
		PlayerStateListener.TeleportDisabled += TeleportButtonDisabled;
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

	private void AttackButtonDisabled(float disabledTime)
	{
		_disabledTimer = Time.time + disabledTime;
	}
	private void StompButtonDisabled(float disabledTime)
	{
		_disabledTimer = Time.time + disabledTime;
	}
	private void TeleportButtonDisabled(float disabledTime)
	{
		_disabledTimer = Time.time + disabledTime;
	}
}
