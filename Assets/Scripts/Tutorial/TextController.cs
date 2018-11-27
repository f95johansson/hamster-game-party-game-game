﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

	public CanvasGroup TextBox;
	public Text _text;
	public Button NextButton;
	private WaitForSecondsOrCancel _currentWaiting = null;
	private Clippy _clippy;

	private bool _tutorialRunning = true;

	private void Start()
	{
		_clippy = FindObjectOfType<Clippy>();

		NextButton.onClick.AddListener(ButtonClicked);
		_clippy.GetComponent<Button>().onClick.AddListener(ClippyClicked);

		StartCoroutine(DelayedNextText());
	}

	IEnumerator DelayedNextText() {
		yield return new WaitForSeconds(0.8f);;
		StartCoroutine(NextText());
	}

	IEnumerator NextText()
    {
		_tutorialRunning = true;

		bool first = true; // I know ugly, but I don't care anymore
		foreach (string text in TutorialText.Tutorial1)
		{
			if (first) {
				first = false;
			} else {
				StartCoroutine(FadeOut(TextBox, 0.5f));
				_currentWaiting = new WaitForSecondsOrCancel(0.5f);
				yield return _currentWaiting;
			}
		
			_text.text = text;

			_clippy.Jump();

			StartCoroutine(FadeIn(TextBox, 0.5f));
			_currentWaiting = new WaitForSecondsOrCancel(0.5f);
			yield return _currentWaiting;
			
        	_currentWaiting = new WaitForSecondsOrCancel(5f);
			yield return _currentWaiting;
		}

		_tutorialRunning = false;
		StartCoroutine(FadeOut(TextBox, 0.5f));
		_currentWaiting = new WaitForSecondsOrCancel(0.5f);
		yield return _currentWaiting;
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

	void ButtonClicked() {
		if (_currentWaiting != null) {
			_currentWaiting.cancel();
		}
	}

	void ClippyClicked() {
		if (!_tutorialRunning) {
			StartCoroutine(NextText());
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
		Debug.Log("canceled");
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