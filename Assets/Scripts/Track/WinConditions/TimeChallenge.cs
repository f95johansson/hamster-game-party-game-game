using UnityEngine.Events;

public class TimeChallenge : WinCondition
{
	private readonly UnityEvent _onProgress = new UnityEvent();
	private readonly UnityEvent _onWin = new UnityEvent();
	private readonly UnityEvent _onTestWin = new UnityEvent();
	
	private CheckPoint[] _checkPoints;
	public uint LapsToWin = 1;
	public int TimeToBeatInSeconds;
	private int _timeLeft;

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

	public override void Restart()
	{
		_timeLeft = TimeToBeatInSeconds * 60;
		_nextCheckPoint.Reset();
		_clearedLaps = 0;
		_onProgress.Invoke();
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

	private uint _clearedLaps;

	private void FixedUpdate()
	{
		_timeLeft--;
		_onProgress.Invoke();
		
		OnStateChange().Invoke();
		var currentLap = GetLap();
		
		if (currentLap > _clearedLaps)
		{
			_clearedLaps = currentLap;
			if (_clearedLaps >= LapsToWin && _timeLeft >= 0)
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
		var lapText = "Clear ";

		if (LapsToWin > 1)
		{
			lapText += LapsToWin + " laps";
		}
		else
		{
			lapText += "a lap";
		}

		return lapText + " in " + TimeToBeatInSeconds + "s";
	}

	public override string ChangedState()
	{
		var time = TimeToBeatInSeconds - _timeLeft / 60f;
		return time.ToString("0.0");
	}
}
