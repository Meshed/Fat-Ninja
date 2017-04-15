using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
				LevelText.text = "0";
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
		GameOverCanvasGroup.alpha = 1;
		GameOverCanvasGroup.interactable = true;
		GameOverCanvasGroup.blocksRaycasts = true;
	}

	public void PlayerDied()
	{
		currentState = LevelStates.GameOver;
		onStateChange (LevelStates.GameOver);
	}
}
