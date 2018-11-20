using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class HamsterState //: MonoBehaviour
{
    
    private string UUID;
    [Range(1, 5)]
    public uint foodLevel = 5; //0,1,2
    [Range(1, 5)]

    public uint WeightLevel = 1; //0,1,2
    [Range(1, 5)]
    public uint SpeedLevel = 1; //0,1,2
    [Range(1, 5)]
    public uint FrictionLevel = 1;
    [Range(1, 5)]
    public uint TurnSpeedLevel = 1;


    //FOOD FUNCTIONS
    public void IncreaseFoodLevel(uint amountIncrease)
    {
        foodLevel += amountIncrease;
    }

    public void DecreaseFoodLevel(uint amountDecrease)
    {
        foodLevel += amountDecrease;
    }




    public void GenerateUUID() {
            DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            int currentEpochTime = (int)(DateTime.UtcNow - epochStart).TotalSeconds;
            int z1 = UnityEngine.Random.Range(0, 1000000);
            int z2 = UnityEngine.Random.Range(0, 1000000);
            UUID = currentEpochTime + ":" + z1 + ":" + z2;
    }

    public string getUUID() {
        return UUID;
    }


}
