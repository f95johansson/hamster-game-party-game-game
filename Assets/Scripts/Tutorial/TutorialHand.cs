using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public class TutorialHand : MonoBehaviour
{
	public Transform _startPosition;
	private Vector2 _goalPosition;
	private CanvasGroup _canvasGroup;
	
	private void Start ()
	{
		var cam = Camera.main;
		Assert.IsNotNull(cam);
		
		var middle = new Vector2(Screen.width / 2.0f, Screen.height / 2.0f);
		_goalPosition = VectorMath.ToWorldPoint(cam, middle, Vector3.zero, Vector3.up);
		_goalPosition = cam.WorldToScreenPoint(_goalPosition);
		_canvasGroup = GetComponent<CanvasGroup>();
	}

	private bool _finished = true;
	private void Update()
	{
		if (_finished) StartCoroutine(DoMove());

		if (FindObjectOfType<PushEffector>() || FindObjectOfType<TurnEffector>())
		{
			Destroy(gameObject);
		}
	}

	private IEnumerator DoMove()
	{
		_finished = false;
		transform.position = _startPosition.position;
		_canvasGroup.alpha = 1;
		
		yield return new WaitForSeconds(1.5f);

		var maxDistance = Vector2.Distance(_startPosition.position, _goalPosition);
		
		var distance = float.MaxValue;

		while(distance > .5f)
		{
			transform.position = Vector2.Lerp(transform.position, _goalPosition, .05f);
			distance = Vector2.Distance(transform.position, _goalPosition);
			_canvasGroup.alpha = Mathf.Pow(distance / maxDistance, 0.25f);
			yield return new WaitForFixedUpdate();
		}

		_canvasGroup.alpha = 0;

		yield return new WaitForSeconds(.5f);
		_finished = true;
	}
}
