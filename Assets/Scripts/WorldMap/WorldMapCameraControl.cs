using System;
using UnityEngine;
using UnityEngine.Assertions;

public class WorldMapCameraControl : MonoBehaviour {

	private Camera _camera;

	public SpriteRenderer WorldMap;

	private Vector3 _mapMax;
	private Vector3 _mapMin;

	private float _startSize;

	public float BorderThickness = 10f;
	public float CamSpeed = 2f;

	private void Start ()
	{
		_camera = Camera.main;
		
		Assert.IsNotNull(_camera);
		
		_startSize = _camera.orthographicSize;
		_previousMousePosition = Input.mousePosition;
		
		_mapMin = WorldMap.bounds.min;
		_mapMax = WorldMap.bounds.max;
		
		_mapMin.z = transform.position.z;
		_mapMax.z = transform.position.z;
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

		var mouseX = _camera.ScreenToWorldPoint(Input.mousePosition).x;
		
		if (mouseX - CameraMin().x < BorderThickness) return new Vector2(-CamSpeed, 0);
		if (CameraMax().x - mouseX < BorderThickness) return new Vector2(CamSpeed, 0);
		
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

	private Vector3 CameraMin()
	{
		return _camera.ScreenToWorldPoint(Vector3.zero);
	}
	
	private Vector3 CameraMax()
	{
		return _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
	}

	private void ClampCameraPosition()
	{
		var currentMin = CameraMin();
		var currentMax = CameraMax();

		var moveR = (currentMin.x < _mapMin.x) ? _mapMin.x - currentMin.x : 0; // positive
		var moveL = (currentMax.x > _mapMax.x) ? _mapMax.x - currentMax.x : 0; // negative
		
		_camera.orthographicSize *= (_mapMax.x - _mapMin.y) / (currentMax.x - currentMin.x);
		if (_camera.orthographicSize > _startSize) _camera.orthographicSize = _startSize;

		_camera.transform.position += new Vector3(moveR + moveL, 0, 0);
	}
}
