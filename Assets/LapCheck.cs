using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

public class LapCheck : MonoBehaviour
{
	private readonly UnityEvent LapMade = new UnityEvent();
	private CheckPoint[] _checkPoints;

	private struct NextCheckPoint
	{
		public int Next;

		public NextCheckPoint(int next)
		{
			Next = next;
		}
	}

	private NextCheckPoint _nextCheckPoint;

	private void Start ()
	{
		_checkPoints = GetComponentsInChildren<CheckPoint>();
		
		_nextCheckPoint = new NextCheckPoint(0);

		for (var i = 0; i < _checkPoints.Length; i++)
		{
			var checkPoint = _checkPoints[i];
			checkPoint.Index = i;
			
			checkPoint.HamsterIsHere.AddListener(index =>
			{
				if (_nextCheckPoint.Next >= index)
				{
					_nextCheckPoint.Next = index + 1;
				}
			});
		}
	}

	private void Update()
	{
		if (_nextCheckPoint.Next >= _checkPoints.Length)
		{
			LapMade.Invoke();
			Debug.Log("Lap made");
		}
	}
	
}
