using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
	public static GameManager Instance { get; private set; }

	private int _currentLevel = 1;

	public int CurrentLevel 
	{ 
		get
		{
			return _currentLevel;
		}
	}

	void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy (gameObject);
		}
		
		Instance = this;
		DontDestroyOnLoad (gameObject);
	}

	public void CompleteLevel()
	{
		_currentLevel++;
	}
}
