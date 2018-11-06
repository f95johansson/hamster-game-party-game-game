using System;

[Serializable]
public class Inventory {

    public const int maxNumberHamsters = 10;
    

    public HamsterState[] hamsterStates = new HamsterState[maxNumberHamsters];

    public uint foodAmount = 0;
    public uint moneyAmount = 100;

    public void AddHamster(HamsterState hamsterToAdd)
    {
        for (int i=0; i<hamsterStates.Length; ++i)
        {
            if (hamsterStates[i]==null)
            {
                hamsterStates[i] = hamsterToAdd;
                return;
            }
        }
    }

    public void RemoveHamster(HamsterState hamsterToRemove)
    {
        for (int i = 0; i < hamsterStates.Length; ++i)
        {
            if (hamsterStates[i] == hamsterToRemove)
            {
                hamsterStates[i] = null;
                return;
            }
        }
    }

    public void AddFood(int amoutToAdd)
    {
        foodAmount += amoutToAdd;
    }

    public void AddMoney(int amoutToAdd)
    {
        moneyAmount += amoutToAdd;
    }

}
