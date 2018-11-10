using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanBlades : MonoBehaviour
{

	public float RotateSpeed = 20f;
	
	// Update is called once per frame
	private void Update ()
	{
		transform.Rotate(new Vector3(0, 0, RotateSpeed * Time.deltaTime));
	}
}
