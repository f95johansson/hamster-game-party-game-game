using UnityEngine;

public class MoveSimple : MonoBehaviour
{
	public Track Track;
	private float _ySpeed;
	public float Gravity = -0.5f;
	public float Speed = 2;
	private float _radius;
	public Transform Hamster;

	private void Start()
	{
		_radius = transform.lossyScale.x / 2;
	}
	
	// Update is called once per frame
	private void Update ()
	{
		if (Input.GetButtonDown("Jump"))
		{
			_ySpeed += 10;
		}
		
		var dx = Input.GetAxis("Horizontal") * Speed;
		var dz = Input.GetAxis("Vertical") * Speed;

		var sp = new Vector3(dx, _ySpeed, dz);

		var horizontalSpeed = new Vector3(dx, 0, dz);
		
		if (horizontalSpeed.magnitude > 0.1)
		{
			var goal = Quaternion.LookRotation(horizontalSpeed);

			Hamster.transform.rotation = Quaternion.Lerp(Hamster.transform.rotation, goal, 0.2f);
		}
		
		transform.Translate(sp * Time.deltaTime);

		if (Track.GroundAt(transform.position) && AtGroundLevel())
		{
			_ySpeed = 0;
			
			var x = transform.position.x;
			var z = transform.position.z;

			if (_ySpeed < 0)
			{
				transform.position = new Vector3(x, Track.transform.position.y + _radius, z);
				_ySpeed = 0;
			}
		} else
		{
			_ySpeed += Gravity;
		}
	}

	private bool AtGroundLevel()
	{
		var top = transform.position.y + _radius;
		var bottom = transform.position.y - _radius;
		var ty = Track.transform.position.y;

		if (bottom > ty) return false;
		if (top < ty) return false;

		return true;
	}
}
