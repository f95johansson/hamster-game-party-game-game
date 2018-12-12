using System.Collections;
using UnityEngine;

public class HamsterStart : MonoBehaviour
{
	public Hamster HamsterPrefab;
	private Hamster _currentHamster;
	private EffectorHolder _holder;

	public bool BlockPlay;

	private void Start() {
		Spawn();

		_holder = FindObjectOfType<EffectorHolder>();
		
		_holder.GoTimeListener.AddListener(() =>
		{
			if (_currentHamster)
			{
				Pause();
				Reset();
			}
			
			Spawn();
		});
	}
	
	public void PlayPauseToggle()
	{
		if (_currentHamster)
		{
			Pause();
		}
		else if (!BlockPlay)
		{
			Spawn();
		}
	}

	public void Spawn()
	{
		_currentHamster = Instantiate(HamsterPrefab, transform.position, transform.rotation);
		_currentHamster.SetStats(Speed, Weight, TurnSpeed, Friction);
	}

	public void Pause()
	{
		if (_currentHamster)
		{
			Destroy(_currentHamster.gameObject);
			_currentHamster = null;
			Reset();
		}
	}

	public uint Speed = 2;
	public uint Friction = 2;
	public uint TurnSpeed = 2;
	public uint Weight = 2;
	private bool _restart;

	public void NewStats(uint speed, uint friction, uint turnSpeed, uint weight)
	{
		Speed = speed;
		Friction = friction;
		TurnSpeed = turnSpeed;
		Weight = weight;
		_restart = true;
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
		
		var timeCheck = FindObjectOfType<TimeChallenge>();
		if (timeCheck)
		{
			timeCheck.Reset();
		}
	
	}

	private void LateUpdate () {

		if (_currentHamster && _currentHamster.transform.position.y < -14 || _restart)
		{
			_restart = false;
			RestartEverything();
		}
		
		if (Input.GetButtonDown("Jump"))
		{
			PlayPauseToggle();
			//un-comment to randomize stats
			//NewStats((uint) Random.RandomRange(1, 6), (uint) Random.RandomRange(1, 6), (uint) Random.RandomRange(1, 6), (uint) Random.RandomRange(1, 6));
		}
	}

	private void RestartEverything()
	{
		Pause();
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
