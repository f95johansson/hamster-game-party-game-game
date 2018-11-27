using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HamsterPrefab : MonoBehaviour
{
    private uint index;
    public GameObject objectTypeToEat;
    public Slider foodBar;


    public void Start()
    {
        foodBar = gameObject.GetComponentInChildren<Slider>();


     }


    public void FixedUpdate()
    {
        foodBar.value = GameControl.Control.Inventory.hamsterStates[index].foodLevel;

    }


    public void UpdateScaleWeight()
    {
        float yScale = gameObject.transform.localScale.y;


        if (GameControl.Control.Inventory.hamsterStates[index].WeightLevel == 0)
        {
            gameObject.transform.localScale = new Vector3(yScale - yScale / 2, yScale, 1);
        }
        else if (GameControl.Control.Inventory.hamsterStates[index].WeightLevel == 1)
        {
            gameObject.transform.localScale = new Vector3(yScale, yScale, 1);
        }
        else if (GameControl.Control.Inventory.hamsterStates[index].WeightLevel == 2)
        {
            gameObject.transform.localScale = new Vector3(yScale + yScale / 2, yScale, 1);
        }
    }

    //COLLIDER
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == objectTypeToEat.name + "(Clone)")
        {
            GameControl.Control.Inventory.hamsterStates[index].foodLevel++;
            
            GameControl.Control.Inventory.RemoveFood(1);


            Destroy(other.gameObject);

        }

    }

    public void setWeightLevel(uint Weigth) {
        GameControl.Control.Inventory.hamsterStates[index].WeightLevel = Weigth;
    }

    public void setIndex(uint i)
    {
        index = i;
        
    }

    
}
