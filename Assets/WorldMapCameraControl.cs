using System;
using UnityEngine;
using UnityEngine.Assertions;

public class WorldMapCameraControl : MonoBehaviour {

	private Camera _camera;

	public SpriteRenderer WorldMap;

	private float _startSize;

	private void Start ()
	{
		_camera = Camera.main;
		
		Assert.IsNotNull(_camera);
		
		_startSize = _camera.orthographicSize;
		_previousMousePosition = Input.mousePosition;
	}

	private Vector2 MoveDelta()
	{
		if (!Input.touchSupported || Application.platform == RuntimePlatform.WebGLPlayer) return MoveDeltaComputers();
		if (Input.touchCount != 2) return Vector2.zero;

		return (Input.touches[0].deltaPosition + Input.touches[1].deltaPosition) / 2;
	}

	private Vector2 _previousMousePosition = Vector2.zero;
	private bool _middlePressed = false;


	private Vector2 MoveDeltaComputers()
	{
		if (_middlePressed && Input.GetMouseButton(2))
		{
			var ret = _previousMousePosition - (Vector2) Input.mousePosition;
			_previousMousePosition = Input.mousePosition;
			return ret;
		}
		
		if (Input.GetMouseButton(2))
		{
			_middlePressed = true;
		}

		_previousMousePosition = Input.mousePosition;
		return Vector2.zero;
	}

	private void Update ()
	{
		Vector3 delta = MoveDelta();
		delta.y = 0;

		var currentCameraPosition = _camera.WorldToScreenPoint(_camera.transform.position);
		_camera.transform.position = _camera.ScreenToWorldPoint(currentCameraPosition + delta);
		ClampCameraPosition();
	}

	private void ClampCameraPosition()
	{	
		var max = WorldMap.bounds.max;
		var min = WorldMap.bounds.min;

		var currentMin = _camera.ScreenToWorldPoint(Vector3.zero);
		var currentMax = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

		var moveR = (currentMin.x < min.x) ? min.x - currentMin.x : 0; // positive
		var moveL = (currentMax.x > max.x) ? max.x - currentMax.x : 0; // negative
		
		_camera.orthographicSize *= (max.x - min.y) / (currentMax.x - currentMin.x);
		if (_camera.orthographicSize > _startSize) _camera.orthographicSize = _startSize;
	

		_camera.transform.position += new Vector3(moveR + moveL, 0, 0);
	}
}
