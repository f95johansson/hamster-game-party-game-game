using UnityEngine;
using UnityEngine.Events;

public class LapCheck : MonoBehaviour
{
	public readonly UnityEvent LapMade = new UnityEvent();
	private CheckPoint[] _checkPoints;

	private struct NextCheckPoint
	{
		public uint Next;

		public NextCheckPoint(uint next)
		{
			Next = next;
		}

		public void Reset()
		{
			Next = 0;
		}
	}

	public void Reset()
	{
		_nextCheckPoint.Reset();
	}

	private NextCheckPoint _nextCheckPoint;

	private void Start ()
	{
		_checkPoints = GetComponentsInChildren<CheckPoint>();
		
		_nextCheckPoint = new NextCheckPoint(0);

		for (uint i = 0; i < _checkPoints.Length; i++)
		{
			var checkPoint = _checkPoints[i];
			checkPoint.Index = i;
			var nrOfCheckPoints = (uint) _checkPoints.Length;
			
			checkPoint.HamsterIsHere.AddListener(index =>
			{
				var testIndex = index;
				var next = _nextCheckPoint.Next;
				while(testIndex <= next)
				{
					_nextCheckPoint.Next = testIndex + 1;
					testIndex += nrOfCheckPoints;
				}
			});
		}
	}

	private uint GetLap()
	{
		return (uint) ( _nextCheckPoint.Next / _checkPoints.Length);
	}

	private uint _clearedLaps; //= 0
	
	private void Update()
	{
		var currentLap = GetLap();
		if (currentLap > _clearedLaps)
		{
			_clearedLaps = currentLap;
			LapMade.Invoke();
			Debug.Log("The hamster cleared lap: " + currentLap);
		}
	}
	
}
