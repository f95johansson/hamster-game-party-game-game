using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterState : MonoBehaviour {

    private string UUID;
    [Range(1, 5)]
    private uint foodLevel = 5; //0,1,2
    [Range (1,5)]
    public uint weightLevel = 1; //0,1,2
    [Range(1, 5)]
    public uint speedlevel = 1; //0,1,2
    [Range(1, 5)]
    public uint Friction = 1;
    [Range(1, 5)]
    public uint TurnSpeed = 1;


    public void IncreaseFoodLevel(uint amountIncrease)
    {
        foodLevel += amountIncrease;
    }

    public void DecreaseFoodLevel(uint amountDecrease)
    {
        foodLevel += amountDecrease;
    }

    public void FixedUpdate()
    {
        UpdateScaleWeight(); //TODO : Not to do everytime, just the first time and when we change the weight
    }

    private void UpdateScaleWeight()
    {
        float yScale = gameObject.transform.localScale.y;

        if (weightLevel == 0)
        {
            gameObject.transform.localScale = new Vector3(yScale - yScale / 2, yScale, 1);
        }
        else if (weightLevel == 1)
        {
            gameObject.transform.localScale = new Vector3(yScale, yScale, 1);
        }
        else if (weightLevel == 2)
        {
            gameObject.transform.localScale = new Vector3(yScale + yScale / 2, yScale, 1);
        }
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
