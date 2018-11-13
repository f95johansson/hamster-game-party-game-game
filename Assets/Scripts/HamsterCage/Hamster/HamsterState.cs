using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HamsterState : MonoBehaviour {

    public GameObject objectTypeToEat;

    public Slider foodbar;

    private uint foodLevel = 2; //0,1,->5
    [Range (0,2)]
    public uint weightLevel = 1; //0,1,2
    [Range(0, 2)]
    public uint fearLevel = 1; //0,1,2




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




}
