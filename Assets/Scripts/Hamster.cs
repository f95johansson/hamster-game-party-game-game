
using UnityEngine;

public class Hamster : MonoBehaviour
{
	private Track _track;
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
	
	private EffectorHolder _effectorHolder;
	
	private Vector3 _force;

	private BallCollision _collision;

	private void Awake()
	{
		_radius = GetComponent<SphereCollider>().radius;
		_collision = new BallCollision(_radius / 3, 8);
	}

	private CoinHandler _coinHandler;
	private void Start()
	{
		_coinHandler = FindObjectOfType<CoinHandler>();
		_effectorHolder = FindObjectOfType<EffectorHolder>();
		_track = FindObjectOfType<Track>();
	}
	
	private void Update()
	{
		Collectibles();
		
		_force += _effectorHolder.GetPushForce(_collision.GetPoints(transform.position));
		
		var turn = _effectorHolder.GetTurnFocus(_collision.GetPoints(transform.position));
		if (turn != Vector3.zero)
		{
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(turn), TurnSpeed);
		}

		_force += (transform.forward * Speed);
		
		_force += VerticalStuff();
		
		transform.position += _force * Weakener;
		
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
			if (distance < _radius + coin.Radius)
			{
				_coinHandler.Add(coin.transform.position);
				Destroy(coin.gameObject);
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		var checkPoint = other.gameObject.GetComponent<CheckPoint>();
		Debug.Log("Some collision");

		if (checkPoint)
		{
			Debug.Log("Checkpoint collision");
			checkPoint.Collided();
		}
	}
}
