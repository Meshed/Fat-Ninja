using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour 
{
	public static SceneHandler Instance { get; private set; }

	void Awake()
	{
		Instance = this;
	}

	public void SwitchToScene(string sceneName)
	{
		SceneManager.LoadScene (sceneName);
	}
}
