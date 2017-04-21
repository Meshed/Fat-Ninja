using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour 
{
	public enum LevelStates
	{
		Setup,
		Intro,
		Running,
		Paused,
		GameOver,
		Complete
	}

	public LevelStates currentState;
	public float IntroDuration = 5.0f;
	public float LevelLength = 30.0f;
	public CanvasGroup GameOverCanvasGroup;
	public CanvasGroup LevelCompleteCanvasGroup;
	public Canvas IntroCanvas;
	public Text LevelText;

	public delegate void LevelManagerHandler(LevelManager.LevelStates newState);
	public static event LevelManagerHandler onStateChange;

	private float _introDelayTimer = 0.0f;
	private float _levelLengthTimer = 0.0f;
	private Animator IntroAnimationReady;
	private Animator IntroAnimationGo;
	private bool _introAnimationRunning = false;

	void Awake()
	{
		IntroAnimationReady = IntroCanvas.GetComponent<Animator> ();
	}

	// Use this for initialization
	void Start () {
		onStateChange (LevelManager.LevelStates.Setup);
	}
	
	void LateUpdate()
	{
		onStateCycle ();
	}

	void onStateCycle()
	{
		switch (currentState) 
		{
			case LevelManager.LevelStates.Setup:
				onStateChange (LevelManager.LevelStates.Intro);
				currentState = LevelStates.Intro;
				_introDelayTimer = Time.time + IntroDuration;
				LevelText.text = GameManager.Instance.CurrentLevel.ToString ();
				break;
			case LevelManager.LevelStates.Intro:
				if (!_introAnimationRunning)
				{
					StartCoroutine (PlayIntro ());
				}
				break;
			case LevelManager.LevelStates.Running:
				if (_levelLengthTimer == 0)
				{
					_levelLengthTimer = Time.time + LevelLength;
				}
				else
				{
					if (Time.time >= _levelLengthTimer)
					{
						currentState = LevelStates.Complete;
						onStateChange (LevelStates.Complete);
					}
				}
				break;
			case LevelManager.LevelStates.Paused:
				break;
			case LevelStates.GameOver:
				DisplayGameOver ();
				break;
			case LevelStates.Complete:
				GameManager.Instance.CompleteLevel ();
				DisplayLevelComplete ();
				break;
		}
	}

	IEnumerator PlayIntro()
	{
		_introAnimationRunning = true;
		IntroAnimationReady.SetTrigger ("Ready");
		yield return new WaitForSeconds (IntroDuration);
		_introAnimationRunning = false;
		onStateChange (LevelManager.LevelStates.Running);
		currentState = LevelStates.Running;
	}

	void DisplayGameOver()
	{
		GameOverCanvasGroup.alpha = 1;
		GameOverCanvasGroup.interactable = true;
		GameOverCanvasGroup.blocksRaycasts = true;
	}
	void HideGameOver()
	{
		GameOverCanvasGroup.alpha = 0;
		GameOverCanvasGroup.interactable = false;
		GameOverCanvasGroup.blocksRaycasts = false;
	}
	void DisplayLevelComplete()
	{
		LevelCompleteCanvasGroup.alpha = 1;
		LevelCompleteCanvasGroup.interactable = true;
		LevelCompleteCanvasGroup.blocksRaycasts = true;
	}

	public void PlayerDied()
	{
		currentState = LevelStates.GameOver;
		onStateChange (LevelStates.GameOver);
	}

	public void SwitchToScene(string sceneName)
	{
		Debug.Log ("Switch To Scene called: " + sceneName);
		SceneManager.LoadScene (sceneName);
	}
}
