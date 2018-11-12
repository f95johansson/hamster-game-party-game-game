using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

	public float RotateSpeed = 20f;
	public float Radius;

	public void Start()
	{
		var sphere = GetComponent<SphereCollider>();
		Radius = sphere.radius;
	}
	
	// Update is called once per frame
	private void Update ()
	{
		transform.Rotate(new Vector3(0, RotateSpeed * Time.deltaTime, 0));
	}
}
