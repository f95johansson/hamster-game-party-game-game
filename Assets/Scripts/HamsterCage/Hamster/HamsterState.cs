using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HamsterState : MonoBehaviour {


    public GameObject objectTypeToEat;

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

    public Slider foodBar;

  

    public void FixedUpdate()
    {
        foodBar.value = foodLevel;

    }


    //FOOD FUNCTIONS
    public void IncreaseFoodLevel(uint amountIncrease)
    {
        foodLevel += amountIncrease;
    }

    public void DecreaseFoodLevel(uint amountDecrease)
    {
        foodLevel += amountDecrease;
    }


    //SCALE WEIGHT
    public void UpdateScaleWeight()
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

    //COLLIDER
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ( other.gameObject.name == objectTypeToEat.name + "(Clone)")
        {
            IncreaseFoodLevel(1);
            GameControl.Control.Inventory.RemoveFood(1);

            
            Destroy(other.gameObject);

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
