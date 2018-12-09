using System;
using System.Linq;

[Serializable]
public class Inventory {
    private const int MaxNumberHamsters = 6;
    public int HamsterOwns = 0;

    public HamsterState[] hamsterStates = new HamsterState[MaxNumberHamsters];

    public uint foodAmount = 0;
    public uint moneyAmount = 100;

    public void AddHamster(HamsterState hamsterToAdd)
    {
        for (var i = 0; i<hamsterStates.Length; ++i)
        {
            if (hamsterStates[i]==null || hamsterStates[i].HamsterName.Length == 0)
            {
                hamsterStates[i] = hamsterToAdd;
                HamsterOwns += 1;
                return;
            }
        }
    }

    public void RemoveHamster(HamsterState hamsterToRemove)
    {
        for (var i = 0; i < hamsterStates.Length; ++i)
        {
            if (hamsterStates[i] == hamsterToRemove)
            {
                hamsterStates[i] = null;
                HamsterOwns -= 1;
                return;
            }
        }
    }

    public void AddFood(uint amountToAdd)
    {
        foodAmount += amountToAdd;
    }

    public void RemoveFood(uint amountToRemove)
    {
        foodAmount -= amountToRemove;
    }

    public void AddMoney(uint amountToAdd)
    {
        moneyAmount += amountToAdd;
    }

    public void RemoveAllHamsters()
    {
        for (var i = 0; i < hamsterStates.Length; ++i)
        {
            hamsterStates[i] = null;
        }
        HamsterOwns = 0;
    }

    public bool HasAHamster()
    {
        return hamsterStates.Any(s => s != null && s.HamsterName.Length > 0);
    }
}
