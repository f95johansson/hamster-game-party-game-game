using UnityEngine;

public class MoveSimple : MonoBehaviour
{
	public Track Track;
	private float _ySpeed;
	public float Gravity = -0.5f;
	public float Speed = 2;
	private float _radius;

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

		transform.transform.Translate(new Vector3(dx, _ySpeed, dz) * Time.deltaTime);

		if (Track.GroundAt(transform.position) && transform.position.y - _radius < Track.transform.position.y)
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
}
