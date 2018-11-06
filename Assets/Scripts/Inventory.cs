using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public const int maxNumberHamsters = 10;
    

    public HamsterState[] hamsterStates = new HamsterState[maxNumberHamsters];

    public float foodAmount;
    public float moneyAmount;

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

    public void AddFood(float amoutToAdd)
    {
        foodAmount += amoutToAdd;
    }

    public void AddMoney(float amoutToAdd)
    {
        moneyAmount += amoutToAdd;
    }

}
