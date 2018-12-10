
using UnityEngine;

public class Hamster : MonoBehaviour
{
	private Track _track;
	private float _ySpeed;
	public float Gravity = -0.5f;
	private float _radius;
	
	public float Speed = 2;
	public float Friction = 0.15f;
	public float TurnSpeed = 0.25f;
	public float Weight = 1f;
	
	private const float Weakener = 1/60f; // this is used to scale all the forces to delta time
	
	private EffectorHolder _effectorHolder;
	
	private Vector3 _force;

	private BallCollision _collision = new BallCollision(0, 0); // just until we create a real ball collision

	private CoinHandler _coinHandler;
	
	private void Start()
	{
		_coinHandler = FindObjectOfType<CoinHandler>();
		_effectorHolder = FindObjectOfType<EffectorHolder>();
		_track = FindObjectOfType<Track>();
		
		_radius = GetComponent<SphereCollider>().radius;
		_collision = new BallCollision(_radius / 3, 8);
	}
	
	private void Update()
	{
		Collectibles();
		
		_force += _effectorHolder.GetPushForce(_collision.GetPoints(transform.position)) * (1/Weight);
		
		var turn = _effectorHolder.GetTurnFocus(_collision.GetPoints(transform.position));
		if (turn != Vector3.zero)
		{
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(turn), TurnSpeed);
		}

		_force += (transform.forward * Speed);
		
		_force += VerticalStuff();

		if (_ySpeed >= 0)
		{
			transform.position += _force * Weakener;
		}
		else
		{
			var newPos = transform.position + (_force * Weakener);

			var collisionHere = _track.GroundAt(transform.position);
			var collisionThere = _track.GroundAt(newPos);

			if (!collisionHere && collisionThere)
			{
				transform.position += new Vector3(0, _force.y, 0) * Weakener;
			}
			else
			{
				transform.position = newPos;
			}
		}

		_force *= 1 - Friction;
	}

	private bool _aboveGround = true;
	private Vector3 VerticalStuff()
	{
		var groundAtXZ = _track.GroundAt(_collision.GetPoints(transform.position));
		
		if (groundAtXZ && _aboveGround && _ySpeed >= 0)
		{
			var x = transform.position.x;
			var z = transform.position.z;
			
			transform.position = new Vector3(x, _track.transform.position.y + _radius, z);
			_ySpeed = 0;
		}
		else
		{
			_ySpeed += Gravity;
		}

		_aboveGround = transform.position.y >= _track.transform.position.y + _radius; 
		return new Vector3(0, _ySpeed, 0);
	}

	
	private void Collectibles()
	{
		foreach (var coin in FindObjectsOfType<Coin>())
		{
			var distance = VectorMath.ToXZ(transform.position - coin.transform.position).magnitude;
			if (distance < (_radius + coin.Radius)/2)
			{
				_coinHandler.Add(coin.transform.position);
				Destroy(coin.gameObject);
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		var checkPoint = other.gameObject.GetComponent<CheckPoint>();

		if (checkPoint)
		{
			checkPoint.Collided();
		}
	}

	private static float Normalize(uint value)
	{
		return value / 5f;
	}
	
	private static float Range(float low, float high, uint value)
	{
		return low + Normalize(value) * (high-low);
	}

	public void SetStats(uint speed, uint weight, uint turnSpeed, uint friction)
	{
		Speed = Range(.5f, 2.2f, speed);
		TurnSpeed = Range(.03f, .09f, turnSpeed);
		Friction = Range(0.05f, .15f, friction);
		Weight = Range(.5f, 2f, weight);
	}
}
