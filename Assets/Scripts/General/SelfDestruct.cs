using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour 
{
	public float SelfDestructDelay = 5.0f;

	private float _selfDestructTimer = 0.0f;
	private LevelManager.LevelStates _currentLevelState;

	void OnEnable()
	{
		LevelManager.onStateChange += OnLevelStateChange;
	}
	void OnDisable()
	{
		LevelManager.onStateChange -= OnLevelStateChange;
	}

	void Start () 
	{
		_selfDestructTimer = Time.time + SelfDestructDelay;
	}
	void Update()
	{
		switch (_currentLevelState)
		{
			case LevelManager.LevelStates.Running:
				if (Time.time >= _selfDestructTimer)
				{
					Destroy (gameObject);
				}
				break;
			case LevelManager.LevelStates.GameOver:
				break;
			case LevelManager.LevelStates.Complete:
				break;
		}
	}

	public void OnLevelStateChange(LevelManager.LevelStates newState)
	{
		_currentLevelState = newState;
	}
}
