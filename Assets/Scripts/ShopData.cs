using System;
using UnityEngine;

[Serializable]
public class ShopData
{
    private DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    public int currentEpochTime;
    public HamsterState[] hamsterStatesShop = new HamsterState[5];
    public uint[] ownHamster = {0, 0, 0, 0, 0};

    public void CheckTime() {
        var newEpochTime = (int)(DateTime.UtcNow - epochStart).TotalSeconds;
        Debug.Log(epochStart);
        Debug.Log(currentEpochTime);
        Debug.Log(newEpochTime);
        if (currentEpochTime == 0) {
            currentEpochTime = newEpochTime;
            ownHamster = new uint[] { 0, 0, 0, 0, 0 };
            for (var i = 0; i < 5; i++)
            {
                hamsterStatesShop[i] = new HamsterState
                {
                    WeightLevel = (uint)UnityEngine.Random.Range(1, 5),
                    SpeedLevel = (uint)UnityEngine.Random.Range(1, 5),
                    FrictionLevel = (uint)UnityEngine.Random.Range(1, 5),
                    TurnSpeedLevel = (uint)UnityEngine.Random.Range(1, 5),
                    HamsterName = BuyFromShopScene2.getRandomName()
                };
                hamsterStatesShop[i].GenerateUUID();
            }
        }
        else if((newEpochTime - currentEpochTime) > 86400) { //one day is 86400 seconds
            currentEpochTime = newEpochTime;
            ownHamster = new uint[] { 0, 0, 0, 0, 0 };
            for (var i = 0; i < 5; i++) {
                hamsterStatesShop[i] = new HamsterState
                {
                    WeightLevel = (uint)UnityEngine.Random.Range(1, 5),
                    SpeedLevel = (uint)UnityEngine.Random.Range(1, 5),
                    FrictionLevel = (uint)UnityEngine.Random.Range(1, 5),
                    TurnSpeedLevel = (uint)UnityEngine.Random.Range(1, 5),
                    HamsterName = BuyFromShopScene2.getRandomName()
                };
                hamsterStatesShop[i].GenerateUUID();
            }
        }
        for (var i = 0; i < 5; i++) {
            if (hamsterStatesShop[i].HamsterName == null)
            {
                hamsterStatesShop[i].HamsterName = BuyFromShopScene2.getRandomName();
            }
        }
            
    }
}