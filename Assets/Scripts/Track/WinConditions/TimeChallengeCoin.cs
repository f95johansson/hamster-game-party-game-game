using UnityEngine.Events;

public class TimeChallengeCoin : WinCondition
{
	private readonly UnityEvent _onWin = new UnityEvent();
	private readonly UnityEvent _onTestWin = new UnityEvent();
	private readonly UnityEvent _onProgress = new UnityEvent();

	public CoinHandler CoinHandler;
	public int TimeToBeatInSeconds;
	private int _timeLeft;
	
    
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
		return "Get the coins in " + TimeToBeatInSeconds + "s";
	}

	public override string ChangedState()
	{
		var time = TimeToBeatInSeconds - _timeLeft / 60f;
		return time.ToString("0.0");
	}

	public void Start()
	{
		ChangedState();
		Restart();
	}

	public override void Restart()
	{
		CoinHandler.ResetCoins();
		_timeLeft = TimeToBeatInSeconds * 60;
		_onProgress.Invoke();
	}

	private uint _nrOfCoins;

	public void FixedUpdate()
	{
		_timeLeft--;
		_onProgress.Invoke();
		
		var total = CoinHandler.CurrentAmount();
		if (_nrOfCoins != total)
		{
			_nrOfCoins = total;
			_onProgress.Invoke();

			if (_nrOfCoins >= CoinHandler.Total() && _timeLeft > 0)
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
}
