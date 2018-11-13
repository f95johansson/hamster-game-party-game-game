﻿using System;
using UnityEngine;

[Serializable]
public class ShopData
{
    private DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    public int currentEpochTime;
    public HamsterState[] hamsterStatesShop = new HamsterState[5];
    public uint[] ownHamster = new uint[] {0, 0, 0, 0, 0};

    //int currentEpochTime = (int)(DateTime.UtcNow - epochStart).TotalSeconds;
    public void CheckTime() {
        int newEpochTime = (int)(DateTime.UtcNow - epochStart).TotalSeconds;
        Debug.Log(epochStart);
        Debug.Log(currentEpochTime);
        Debug.Log(newEpochTime);
        if (currentEpochTime == 0) {
            currentEpochTime = newEpochTime;
        }else if((newEpochTime - currentEpochTime) > 86400) { //one day is 86400 sekunds
            currentEpochTime = newEpochTime;
            ownHamster = new uint[] { 0, 0, 0, 0, 0 };
            for (int i = 0; i < 5; i++) {
                hamsterStatesShop[i] = new HamsterState
                {
                    weightLevel = (uint)UnityEngine.Random.Range(1, 5),
                    speedlevel = (uint)UnityEngine.Random.Range(1, 5),
                    Friction = (uint)UnityEngine.Random.Range(1, 5),
                    TurnSpeed = (uint)UnityEngine.Random.Range(1, 5)
                };
            }
        }
    }
}