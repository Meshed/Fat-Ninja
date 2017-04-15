using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour 
{
	public enum EnemySpawnerStates
	{
		Idle,
		Running
	}

	public GameObject Player;
	public GameObject[] Enemies;
	public float SpawnDelay = 5;
	public float SpawnOffset = 20.0f;

	private float SpawnDelayTimer = 0.0f;

	private EnemySpawnerStates _currentEnemeySpawnerState;

	void OnEnable()
	{
		LevelManager.onStateChange += OnLevelStateChange;
	}

	void OnDisable()
	{
		LevelManager.onStateChange -= OnLevelStateChange;
	}

	void Update()
	{
		switch (_currentEnemeySpawnerState)
		{
			case EnemySpawnerStates.Idle:
				break;
			case EnemySpawnerStates.Running:
				SpawnEnemy ();
				break;
		}
	}

	void OnLevelStateChange(LevelManager.LevelStates newState)
	{
		switch (newState)
		{
			case LevelManager.LevelStates.Setup:
				_currentEnemeySpawnerState = EnemySpawnerStates.Idle;
				break;
			case LevelManager.LevelStates.Intro:
				_currentEnemeySpawnerState = EnemySpawnerStates.Idle;
				break;
			case LevelManager.LevelStates.Running:
				_currentEnemeySpawnerState = EnemySpawnerStates.Running;
				break;
			case LevelManager.LevelStates.Paused:
				_currentEnemeySpawnerState = EnemySpawnerStates.Idle;
				break;
			case LevelManager.LevelStates.GameOver:
				_currentEnemeySpawnerState = EnemySpawnerStates.Idle;
				break;
			case LevelManager.LevelStates.Complete:
				_currentEnemeySpawnerState = EnemySpawnerStates.Idle;
				break;
		}
	}

	private void SpawnEnemy()
	{
		Vector3 playerPosition = Player.transform.position;
		playerPosition = new Vector3 (playerPosition.x + SpawnOffset, 0, 0);

		GameObject enemyToSpawn = Enemies [Random.Range(0, Enemies.Length)];

		if (SpawnDelayTimer == 0.0f)
		{
			GameObject.Instantiate (enemyToSpawn, playerPosition, Quaternion.identity);
			SpawnDelayTimer = Time.time + SpawnDelay;
		}
		else if (Time.time >= SpawnDelayTimer)
		{
			GameObject.Instantiate (enemyToSpawn, playerPosition, Quaternion.identity);
			SpawnDelayTimer = Time.time + SpawnDelay;
		}
	}
}
