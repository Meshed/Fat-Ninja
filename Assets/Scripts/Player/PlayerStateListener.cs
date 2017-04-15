using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateListener : MonoBehaviour 
{
	public LevelManager levelManager;
	public Transform WeaponSpawn;
	public GameObject Weapon;
	public GameObject RaycastPosition;
	public PlayerStateController Controller;

	public float RunSpeed = 0.05f;
	public float TeleportDistance = 3.0f;
	public float ButtSmashHeight = 5.0f;
	public float AttackCooldown = 5.0f;
	public float StompCooldown = 5.0f;
	public float TeleportCooldown = 5.0f;

	public PlayerStateController.PlayerStates _currentPlayerState;
	private PlayerStateController.PlayerStates _previousPlayerState;

	public delegate void PlayerListenerHandler(float disableTime);
	public static event PlayerListenerHandler AttackDisabled;
	public static event PlayerListenerHandler StompDisabled;
	public static event PlayerListenerHandler TeleportDisabled;

	void OnEnable()
	{
		PlayerStateController.onStateChange += OnPlayerStateChange;
	}
	void OnDisable()
	{
		PlayerStateController.onStateChange -= OnPlayerStateChange;
	}
	void LateUpdate () 
	{
		OnStateCycle ();
	}

	private void OnStateCycle()
	{
		switch (_currentPlayerState)
		{
			case PlayerStateController.PlayerStates.Idle:
				break;
			case PlayerStateController.PlayerStates.Running:
				transform.Translate (new Vector3 (RunSpeed, 0, 0));
				break;
			case PlayerStateController.PlayerStates.Attack:
				GameObject.Instantiate (Weapon, WeaponSpawn);
				OnPlayerStateChange (_previousPlayerState);
				break;
			case PlayerStateController.PlayerStates.Dead:
				break;
			case PlayerStateController.PlayerStates.Stomp:
				break;
			case PlayerStateController.PlayerStates.Teleport:
				transform.Translate (new Vector3 (TeleportDistance, 0, 0));
				OnPlayerStateChange (PlayerStateController.PlayerStates.Running);
				break;
		}
	}
	private void OnPlayerStateChange(PlayerStateController.PlayerStates newState)
	{
		if (!IsStateChangeAllowed (newState))
			return;

		switch (newState)
		{
			case PlayerStateController.PlayerStates.Idle:
				break;
			case PlayerStateController.PlayerStates.Running:
				break;
			case PlayerStateController.PlayerStates.Dead:
				break;
			case PlayerStateController.PlayerStates.Attack:
				AttackDisabled (AttackCooldown);
				break;
			case PlayerStateController.PlayerStates.Stomp:
				RaycastHit2D hit = Physics2D.Raycast (RaycastPosition.transform.position, Vector2.right, 5);

				if (hit != null && hit.transform.tag == "Enemy")
				{
					Vector3 buttSmashPosition = hit.transform.position;
					buttSmashPosition.y += ButtSmashHeight;
					transform.position = buttSmashPosition;
					StompDisabled (StompCooldown);
				}
				else
					Debug.Log (hit.transform.tag);
				break;
			case PlayerStateController.PlayerStates.Teleport:
				TeleportDisabled (TeleportCooldown);
				break;
		}
		
		_previousPlayerState = _currentPlayerState;
		_currentPlayerState = newState;
	}

	private bool IsStateChangeAllowed(PlayerStateController.PlayerStates newState)
	{
		bool returnValue = false;

		switch (_currentPlayerState)
		{
			case PlayerStateController.PlayerStates.Idle:
				if (newState == PlayerStateController.PlayerStates.Running)
					returnValue = true;
				break;
			case PlayerStateController.PlayerStates.Running:
				returnValue = true;
				break;
			case PlayerStateController.PlayerStates.Dead:
				if (newState == PlayerStateController.PlayerStates.Idle)
					returnValue = true;
				break;
			case PlayerStateController.PlayerStates.Attack:
				if (newState == PlayerStateController.PlayerStates.Running ||
					newState == PlayerStateController.PlayerStates.Dead)
					returnValue = true;
				break;
			case PlayerStateController.PlayerStates.Stomp:
				if (newState == PlayerStateController.PlayerStates.Running ||
					newState == PlayerStateController.PlayerStates.Dead)
					returnValue = true;
				break;
			case PlayerStateController.PlayerStates.Teleport:
				if (newState == PlayerStateController.PlayerStates.Running ||
					newState == PlayerStateController.PlayerStates.Dead)
					returnValue = true;
				break;
		}

		return returnValue;
	}

	public void HitDeathTrigger()
	{
		levelManager.SendMessage ("PlayerDied", SendMessageOptions.DontRequireReceiver);
		OnPlayerStateChange (PlayerStateController.PlayerStates.Dead);
	}
}
