﻿using System;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraMovement : MonoBehaviour
{

	public float Speed = 0.5f;
	public Transform Target;
	public float Low = 10;
	private Vector3 _targetPoint;

	private void Start()
	{
		var plane = new Plane(Vector3.up, Target.transform.position);
		var ray = GetComponent<Camera>().ScreenPointToRay(new Vector2(Screen.width/2f, Screen.height/2f));

		float distance;
		Assert.IsTrue(plane.Raycast(ray, out distance));
		
		_targetPoint = ray.GetPoint(distance);
	}
	
	private void Update()
	{
		var zoom = Input.mouseScrollDelta.y;
		transform.position = new Vector3(transform.position.x, Math.Max(Low, transform.position.y + zoom * Speed), transform.position.z);

//		var rotation = Input.mouseScrollDelta.x;
//		transform.RotateAround(_targetPoint, new Vector3(0, 1, 0), rotation);

		transform.rotation = Quaternion.LookRotation(_targetPoint - transform.position);
	}
}
