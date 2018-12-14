using UnityEngine.Events;

public class LapCheck : WinCondition
{
	private readonly UnityEvent _onProgress = new UnityEvent();
	private readonly UnityEvent _onWin = new UnityEvent();
	private readonly UnityEvent _onTestWin = new UnityEvent();
	
	private CheckPoint[] _checkPoints;
	public uint LapsToWin = 2;

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
			_onProgress.Invoke();
			if (_clearedLaps >= LapsToWin)
			{
				if (FindObjectOfType<EffectorHolder>().IsGoTime())
				{
					_onWin.Invoke();
				}
				else
				{
					_onTestWin.Invoke();
				}
			}
		}
	}

	public override UnityEvent OnWin()
	{
		return _onWin;
	}

	public override UnityEvent OnTestWin()
	{
		return _onTestWin;
	}

	public override UnityEvent OnStateChange()
	{
		return _onProgress;
	}

	public override string Description()
	{
		return "Complete " + LapsToWin + " lap" + (LapsToWin > 1 ? "s" : "") + " to win";
	}

	public override string ChangedState()
	{
		return "" + _clearedLaps + "/" + LapsToWin;
	}

	public override void Restart()
	{
		_nextCheckPoint.Reset();
		_clearedLaps = 0;
		_onProgress.Invoke();
	}
}
