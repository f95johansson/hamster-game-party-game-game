using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CoinHandler : MonoBehaviour
{
	private readonly HashSet<Vector3> _coinPositions = new HashSet<Vector3>();
	public Coin CoinPrefab;

	private uint _total;

	public void Start()
	{
		_total = (uint) FindObjectsOfType<Coin>().Length;
	}
	
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

	public uint CurrentAmount()
	{
		return (uint) _coinPositions.Count;
	}
	
	public uint Total()
	{
		return _total;
	}
}
