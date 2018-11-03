﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterState : MonoBehaviour {

    private float foodLevel; //0,1,2
    public float weightLevel; //0,1,2
    public float fearLevel; //0,1,2

    public void IncreaseFoodLevel(float amountIncrease)
    {
        foodLevel += amountIncrease;
    }

    public void DecreaseFoodLevel(float amountDecrease)
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


}
