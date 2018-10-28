using UnityEngine;

public class Hamster : MonoBehaviour
{
	public Track Track;
	private float _ySpeed;
	public float Gravity = -0.5f;
	private float _radius;
	
	[Range(0, 10)]
	public float Speed = 2;

	[Range(0, 1)]
	public float Friction = 0.15f;
	
	[Range(0, 1)]
	public float TurnSpeed = 0.25f;

	private const float Weakener = 1/60f; // this is used to scale all the forces to delta time
	
	public EffectorHolder EffectorHolder;
	
	private Vector3 _force;

	private void Start()
	{
		_radius = transform.lossyScale.x / 2;
	}
	
	private void Update()
	{
		_force += EffectorHolder.GetPushForce(transform.position);
		var turn = EffectorHolder.GetTurnFocus(transform.position, _radius);

		if (turn != Vector3.zero)
		{
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(turn), TurnSpeed);
		}

		_force += (transform.forward * Speed);
		
		VerticalStuff();
		
		transform.position += _force * Weakener;
		
		_force *= 1 - Friction;
	}

	private void VerticalStuff()
	{
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
		}
		else
		{
			_ySpeed += Gravity;
		}
		
		_force += new Vector3(0, _ySpeed, 0);
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
