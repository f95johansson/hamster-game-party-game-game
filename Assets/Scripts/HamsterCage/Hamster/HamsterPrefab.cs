using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HamsterPrefab : MonoBehaviour
{
    private uint index;
    private HamsterState state;
    public GameObject objectTypeToEat;
    public Slider foodBar;

    public void FixedUpdate()
    {
        foodBar.value = state.foodLevel;

    }

    public void UpdateScaleWeight()
    {
        float yScale = gameObject.transform.localScale.y;

        if (state.WeightLevel == 0)
        {
            gameObject.transform.localScale = new Vector3(yScale - yScale / 2, yScale, 1);
        }
        else if (state.WeightLevel == 1)
        {
            gameObject.transform.localScale = new Vector3(yScale, yScale, 1);
        }
        else if (state.WeightLevel == 2)
        {
            gameObject.transform.localScale = new Vector3(yScale + yScale / 2, yScale, 1);
        }
    }

    //COLLIDER
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == objectTypeToEat.name + "(Clone)")
        {
            state.IncreaseFoodLevel(1);
            GameControl.Control.Inventory.RemoveFood(1);


            Destroy(other.gameObject);

        }

    }

    public void setWeightLevel(uint Weigth) {
        state.WeightLevel = Weigth;
    }

    public void Start()
    {
        state = GameControl.Control.Inventory.hamsterStates[index];
    }
}
