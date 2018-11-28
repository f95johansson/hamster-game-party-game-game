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

    public Image destroyer;
    

    private bool isGrabbed = false;
    //private bool isDropped = false;


    public void Start()
    {
        foodBar = gameObject.GetComponentInChildren<Slider>();
        
    }

    private void OnMouseDown()
    {
        isGrabbed = true;
        //isDropped = false;
    }



    void Update()
    {


        if (isGrabbed)
        {
           
            var mousePos = Input.mousePosition;

            var mousePosWorld = VectorMath.ToWorldPoint(Camera.main, mousePos, Vector3.zero, new Vector3(0, 0, 1));
            this.gameObject.transform.position = (Vector2)mousePosWorld;


            if (Input.GetMouseButtonUp(0))
            {
                isGrabbed = false;
                //isDropped = true;
                //Is it on the trash ?
            }
        }
        
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
