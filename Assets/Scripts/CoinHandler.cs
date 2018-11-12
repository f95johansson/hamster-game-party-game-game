using System.Collections.Generic;
using UnityEngine;

public class CoinHandler : MonoBehaviour
{
	private readonly HashSet<Vector3> _coinPositions = new HashSet<Vector3>();
	public Coin CoinPrefab;
	
	public void ResetCoins()
	{
		foreach (var c in _coinPositions)
		{
			var coin = Instantiate(CoinPrefab);
			coin.transform.parent = transform;
			coin.transform.position = c;
		}
		_coinPositions.Clear();
	}

	public void Add(Vector3 position)
	{
		_coinPositions.Add(position);
	}
}
