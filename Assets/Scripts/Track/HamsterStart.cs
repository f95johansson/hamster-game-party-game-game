using UnityEngine;

public class HamsterStart : MonoBehaviour
{
	public Hamster HamsterPrefab;
	private Hamster _currentHamster;
	private EffectorHolder _holder;

	private void Start() {
		Spawn();

		_holder = FindObjectOfType<EffectorHolder>();
		
		_holder.GoTimeListener.AddListener(() =>
		{
			if (_currentHamster)
			{
				DestroyCurrentHamster();
				Reset();
			}
			
			Spawn();
		});
	}
	
	public void PlayPauseToggle()
	{
		if (_currentHamster)
		{
			DestroyCurrentHamster();
		}
		else
		{
			Spawn();
		}
	}

	public void Spawn()
	{
		_currentHamster = Instantiate(HamsterPrefab, transform.position, transform.rotation);
		_currentHamster.SetStats(Speed, Weight, TurnSpeed, Friction);
	}

	private void DestroyCurrentHamster()
	{
		Destroy(_currentHamster.gameObject);
		_currentHamster = null;
		Reset();
	}

	public uint Speed = 2;
	public uint Friction = 2;
	public uint TurnSpeed = 2;
	public uint Weight = 2;

	public void NewStats(uint speed, uint friction, uint turnSpeed, uint weight)
	{
		Speed = speed;
		Friction = friction;
		TurnSpeed = turnSpeed;
		Weight = weight;
		
		RestartEverything();
	}

	private void Reset()
	{
		var coinHandler = FindObjectOfType<CoinHandler>();
		if (coinHandler)
		{
			coinHandler.ResetCoins();	
		}
		
		var lapCheck = FindObjectOfType<LapCheck>();
		if (lapCheck)
		{
			lapCheck.Reset();
		}
	}

	private void LateUpdate () {

		if (_currentHamster && _currentHamster.transform.position.y < -10)
		{
			RestartEverything();
		}
		
		if (Input.GetButtonDown("Jump"))
		{
			PlayPauseToggle();
		}
	}

	private void RestartEverything()
	{
		DestroyCurrentHamster();
		_holder.HamsterDied();
		Reset();
		Spawn();
	}

	public bool HasHamster()
	{
		return _currentHamster;
	}

	public Transform CurrentHamsterTransform()
	{
		return _currentHamster.transform;
	}
}
