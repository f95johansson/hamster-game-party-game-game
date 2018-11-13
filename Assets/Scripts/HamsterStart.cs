using UnityEngine;

public class HamsterStart : MonoBehaviour
{
	public Hamster HamsterPrefab;
	private Hamster _currentHamster;

	private void Start() {
		Spawn();
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
		
		Debug.Log(_currentHamster);
	}

	public void Spawn()
	{
		_currentHamster = Instantiate(HamsterPrefab, transform.position, transform.rotation);
	}

	private void DestroyCurrentHamster()
	{
		Destroy(_currentHamster.gameObject);
		_currentHamster = null;
	}

	private void ResetCoins()
	{
		var coinHandler = FindObjectOfType<CoinHandler>();
		coinHandler.ResetCoins();
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
