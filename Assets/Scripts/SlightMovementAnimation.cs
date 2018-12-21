using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlightMovementAnimation : MonoBehaviour {

	public float XMovement = 1;
	public float YMovement = 1;
	public int XOffset = 20;
	public int YOffset = 20;

	private Vector3 _originalPosition;
	private int _counterX = 0;
	private int _counterY = 0;
	private float pi = Mathf.PI;

	// Use this for initialization
	void Start () {
		_originalPosition = transform.position;
		_counterX = XOffset;
		_counterY = YOffset;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		_counterX = (_counterX + 1) % 200;
		_counterY = (_counterY + 1) % 200;

		var easeX = (.5f*Mathf.Sin(pi*(_counterX/100f-pi/6))+.5f)*100f;
		var easeY = (.5f*Mathf.Sin(pi*(_counterY/100f-pi/6))+.5f)*100f;
		var dx = Mathf.Lerp(-XMovement/2, XMovement/2, easeX/100f);
		var dy = Mathf.Lerp(-YMovement/2, YMovement/2, easeY/100f);
		transform.position = new Vector3(_originalPosition.x + dx, _originalPosition.y + dy, _originalPosition.z);
	}
}

/*
	public float XMovement = 10;
	public float YMovement = 10;
	public int XYOffset = 20;

	private Vector3 _originalPosition;
	private int _counterX = 0;
	private int _counterY = 0;
	private int _flipX = 1;
	private int _flipY = 1;

	// Use this for initialization
	void Start () {
		_originalPosition = transform.position;
		_counterY = XYOffset;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		_counterX += 1;
		if (_counterX >= 100) {
			_counterX = 0;
			_flipX *= -1;
		}

		_counterY += 1;
		if (_counterY >= 100) {
			_counterY = 0;
			_flipY *= -1;
		}

		var easeX = (1-Mathf.Sqrt(-Mathf.Pow(_counterX/100f, 2)+1))*100f;
		var easeY = (1-Mathf.Sqrt(-Mathf.Pow(_counterY/100f, 2)+1))*100f;
		var dx = Mathf.Lerp(-XMovement/2*_flipX, XMovement/2*_flipX, easeX/100f);
		var dy = 0;//Mathf.Lerp(-YMovement/2*_flipY, YMovement/2*_flipY, easeY/100f);
		transform.position = new Vector3(_originalPosition.x + dx, _originalPosition.y + dy, _originalPosition.z);
	}
 */