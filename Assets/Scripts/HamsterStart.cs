using UnityEngine;

public class HamsterStart : MonoBehaviour
{
	public Hamster HamsterPrefab;
	private Hamster _currentHamster;
	public EffectorHolder Holder;

	private void Start() {
		Spawn();
		
		Holder.GoTimeListener.AddListener(() =>
		{
			if (_currentHamster)
			{
				DestroyCurrentHamster();
				ResetCoins();
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
		Holder.HamsterDied();
		Destroy(_currentHamster.gameObject);
		_currentHamster = null;
	}

	private static void ResetCoins()
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
			ResetCoins();
			Spawn();
		}
		
		if (Input.GetButtonDown("Jump"))
		{
			PlayPauseToggle();
		}
	}
	
}
