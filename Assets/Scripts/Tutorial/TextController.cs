using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

	public CanvasGroup TextBox;
	public Text _text;
	public Button NextButton;
	private WaitForCancel _currentWaiting = null;
	private Clippy _clippy;
	private WinCondition _winCondition;
	
	public int TutorialNumber = 0;
	public bool ShouldRunEndOfTutorial = false;


	private const float FADE_TIME = 0.5f;


	private void Start()
	{
		_winCondition = FindObjectOfType<WinCondition>();
		
		_clippy = FindObjectOfType<Clippy>();
		NextButton.onClick.AddListener(NextClicked);
		_clippy.GetComponent<Button>().onClick.AddListener(ClippyClicked);

		if (ShouldRunEndOfTutorial) {
			_winCondition.OnWin().AddListener(() => {
				if (_currentWaiting != null) {
					_currentWaiting.cancel();
				}
				StartCoroutine(RunTutorial(TutorialText.Completed));
			});
		}

		StartCoroutine(DelayedNextText(TutorialText.Tutorial[TutorialNumber-1]));
	}

	IEnumerator DelayedNextText(string[] monolog) {
		yield return new WaitForSeconds(0.8f);;
		StartCoroutine(RunTutorial(monolog));
	}

	IEnumerator RunTutorial(string[] monolog)
    {

		bool first = true;
		foreach (string text in monolog)
		{
			// only fade out if not first
			if (first) {
				first = false;
			} else {
				StartCoroutine(FadeOut(TextBox, FADE_TIME));
				yield return new WaitForSeconds(FADE_TIME);
			}
		
			_text.text = text;

			_clippy.Jump();

			StartCoroutine(FadeIn(TextBox, FADE_TIME));
			yield return new WaitForSeconds(FADE_TIME);
			
        	_currentWaiting = new WaitForCancel();
			yield return _currentWaiting;
			_currentWaiting = null;
		}

		StartCoroutine(FadeOut(TextBox, FADE_TIME));
		yield return new WaitForSeconds(FADE_TIME);
    }

	private IEnumerator FadeOut(CanvasGroup objekt, float time)  // seconds
	{
		objekt.alpha = 1;
		while (objekt.alpha > 0) {
			objekt.alpha -= Time.fixedDeltaTime/time;
			yield return new WaitForFixedUpdate();
		}
		objekt.alpha = 0;
		yield return null;
	}

	private IEnumerator FadeIn(CanvasGroup objekt, float time)  // seconds
	{
		objekt.alpha = 0;
		while (objekt.alpha < 1) {
			objekt.alpha += Time.fixedDeltaTime/time;
			yield return new WaitForFixedUpdate();
		}
		objekt.alpha = 1;
		yield return null;
	}

	void NextClicked() {
		if (_currentWaiting != null) {
			_currentWaiting.cancel();
		}
	}

	void ClippyClicked() {
		if (_currentWaiting != null) {
			_currentWaiting.cancel();
		} else {
			StartCoroutine(RunTutorial(TutorialText.Tutorial[TutorialNumber-1]));
		}
	}

	public void NextLevel() {
		if (SceneManager.GetActiveScene().name == "IntroTrack")
		{
			Navigation.StartTrack("IntroTrack2");
		} 
		else if (SceneManager.GetActiveScene().name == "IntroTrack2")
		{
			Navigation.StartTrack("IntroTrack3");
		} 
		else if (SceneManager.GetActiveScene().name == "IntroTrack3")
		{
			Navigation.StartTrack("IntroTrack4");
		}
		else
		{
			Navigation.StartLevelSelect();	
		}

	}
}


class WaitForSecondsOrCancel : CustomYieldInstruction
{
	private float numSeconds;
	private float startTime;
	private bool canceled = false;
 
	public WaitForSecondsOrCancel(float numSeconds)
	{
		startTime = Time.time;
		this.numSeconds = numSeconds;
	}

	public void cancel() {
		canceled = true;
	}
 
	public override bool keepWaiting
	{
		get
		{
			return Time.time - startTime < numSeconds
				&& !canceled;
		}
	}
}

class WaitForCancel : CustomYieldInstruction
{
	private bool canceled = false;
 
	public WaitForCancel()
	{
	}

	public void cancel() {
		canceled = true;
	}
 
	public override bool keepWaiting
	{
		get
		{
			return !canceled;
		}
	}
}