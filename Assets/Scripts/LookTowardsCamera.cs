using UnityEngine;

public class LookTowardsCamera : MonoBehaviour {
	private Camera _camera;

	private void Start()
	{
		_camera = Camera.main;
	}

	private void Update ()
	{
		var pos = _camera.transform.position - transform.position;
		pos.y = 0;

		transform.rotation = Quaternion.LookRotation(pos);
	}
}
