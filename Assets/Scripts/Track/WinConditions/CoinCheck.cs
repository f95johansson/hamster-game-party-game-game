using UnityEngine.Events;

public class CoinCheck : WinCondition {
    private readonly UnityEvent _onWin = new UnityEvent();
    private readonly UnityEvent _onTestWin = new UnityEvent();
    private readonly UnityEvent _onStateChange = new UnityEvent();

    public CoinHandler CoinHandler;
    
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
        return _onStateChange;
    }

    public override string Description()
    {
        return "Gather all the coins";
    }

    public override string ChangedState()
    {
        return CoinHandler.CurrentAmount() + "/" + CoinHandler.Total();
    }

    public void Start()
    {
        ChangedState();
    }

    private uint _nrOfCoins;

    public void Update()
    {
        var total = CoinHandler.CurrentAmount();
        if (_nrOfCoins != total)
        {
            _nrOfCoins = total;
            _onStateChange.Invoke();

            if (_nrOfCoins >= CoinHandler.Total())
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
