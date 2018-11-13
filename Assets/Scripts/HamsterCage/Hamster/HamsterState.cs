using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HamsterState : MonoBehaviour {

<<<<<<< HEAD:Assets/Scripts/HamsterCage/Hamster/HamsterState.cs
    public GameObject objectTypeToEat;

    public Slider foodbar;

    private uint foodLevel = 2; //0,1,->5
    [Range (0,2)]
=======
    private string UUID;
    [Range(1, 5)]
    private uint foodLevel = 5; //0,1,2
    [Range (1,5)]
>>>>>>> d7dbb31b9a46803fcf4da56abf205f31c8174970:Assets/Scripts/Hamster/HamsterState.cs
    public uint weightLevel = 1; //0,1,2
    [Range(0, 2)]
    public uint fearLevel = 1; //0,1,2
    [Range(1, 5)]
    public uint speedlevel = 1; //0,1,2
    [Range(1, 5)]
    public uint Friction = 1;
    [Range(1, 5)]
    public uint TurnSpeed = 1;





    //PERSISTENCE
    private void Awake()
    {
        Debug.Log(Application.persistentDataPath);
        GameControl.Control.LoadInventory();
        GameControl.Control.LoadPlayerData();
    }

    private void OnDestroy()
    {
        GameControl.Control.SaveInventory();
        GameControl.Control.SavePlayerData();
    }


    public void FixedUpdate()
    {
        
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

<<<<<<< HEAD:Assets/Scripts/HamsterCage/Hamster/HamsterState.cs
    public void UpdateFoodBar()
    {
        foodbar.value = foodLevel;
       //Change color!
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



=======
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

>>>>>>> d7dbb31b9a46803fcf4da56abf205f31c8174970:Assets/Scripts/Hamster/HamsterState.cs

}
