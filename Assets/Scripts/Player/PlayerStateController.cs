using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour 
{
	public enum PlayerStates
	{
		Idle,
		Running,
		Dead,
		Attack,
		Stomp,
		Teleport
	}

	public LevelManager.LevelStates _currentLevelState;
	public PlayerStates _currentPlayerState;
	public float StompAnimationDelay = 0.5f;

	public delegate void PlayerStateHandler(PlayerStateController.PlayerStates newState);
	public static event PlayerStateHandler onStateChange; 

	private float _stompAnimationTimer = 0.0f;

	void OnEnable()
	{
		LevelManager.onStateChange += OnLevelStateChange;
	}
	void OnDisable()
	{
		LevelManager.onStateChange -= OnLevelStateChange;
	}
	void LateUpdate()
	{
		switch (_currentPlayerState)
		{
			case PlayerStates.Idle:
			case PlayerStates.Running:
			case PlayerStates.Dead:
			case PlayerStates.Attack:
				break;
			case PlayerStates.Stomp:
				if (transform.position.y < 0.02f)
				{
					if (_stompAnimationTimer == 0.0f)
					{
						_stompAnimationTimer = Time.time + StompAnimationDelay;
					}
					else if (Time.time >= _stompAnimationTimer)
					{
						_stompAnimationTimer = 0.0f;
						_currentPlayerState = PlayerStates.Running;
						onStateChange (PlayerStateController.PlayerStates.Running);
					}
				}
				break;
			case PlayerStates.Teleport:
				break;
		}
	}
	void OnLevelStateChange(LevelManager.LevelStates newState)
	{
		switch (newState)
		{
			case LevelManager.LevelStates.Setup:
				onStateChange (PlayerStateController.PlayerStates.Idle);
				break;
			case LevelManager.LevelStates.Intro:
				onStateChange (PlayerStateController.PlayerStates.Idle);
				break;
			case LevelManager.LevelStates.Running:
				onStateChange (PlayerStates.Running);
				break;
			case LevelManager.LevelStates.Paused:
				onStateChange (PlayerStates.Idle);
				break;
			case LevelManager.LevelStates.GameOver:
				onStateChange (PlayerStates.Idle);
				break;
			case LevelManager.LevelStates.Complete:
				onStateChange (PlayerStates.Idle);
				break;
		}

		_currentLevelState = newState;
	}

	public void TeleportClicked()
	{
		_currentPlayerState = PlayerStates.Teleport;
		onStateChange (PlayerStateController.PlayerStates.Teleport);
	}
	public void AttackClicked()
	{
		_currentPlayerState = PlayerStates.Attack;
		onStateChange (PlayerStateController.PlayerStates.Attack);
	}
	public void ButtSmashClicked()
	{
		_currentPlayerState = PlayerStates.Stomp;
		onStateChange (PlayerStateController.PlayerStates.Stomp);
	}
}
