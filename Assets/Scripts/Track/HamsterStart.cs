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
	}

	private void DestroyCurrentHamster()
	{
		Destroy(_currentHamster.gameObject);
		_currentHamster = null;
		Reset();
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
			DestroyCurrentHamster();
			_holder.HamsterDied();
			Reset();
			Spawn();
		}
		
		if (Input.GetButtonDown("Jump"))
		{
			PlayPauseToggle();
		}
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
