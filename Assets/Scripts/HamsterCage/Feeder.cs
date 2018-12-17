using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Feeder : MonoBehaviour {

    public Component foodImage;
    public GameObject foodPrefab;
    public Text missingFood;
    public Text foodAmountText;

    private GameObject newFood;
    private Camera camera; 


    // Use this for initialization
    void Start () {
        camera = Camera.main;
        UpdateFoodText();

        Events.OnEvent(UnityEngine.EventSystems.EventTriggerType.PointerDown, foodImage, e =>
        {
            if (GameControl.Control.Inventory.foodAmount > 0)
            {
                newFood = Instantiate(foodPrefab);
            }
            
        });

    }
	
	// Update is called once per frame
	void Update () {
        

        if (newFood)
        {
            var mousePos = Input.mousePosition;

            var mousePosWorld = VectorMath.ToWorldPoint(camera, mousePos, Vector3.zero, new Vector3(0,0,1));
            newFood.transform.position = (Vector2) mousePosWorld;
            

            if (Input.GetMouseButtonUp(0))
            {
                newFood = null;
                UpdateFoodText();
            }
        }

        if(GameControl.Control.Inventory.foodAmount > 0)
        {
            missingFood.text = "";
            
        }
        else
        {
            missingFood.text = "You don't have food anymore!";
        }


    }


    public void UpdateFoodText()
    {
        foodAmountText.text = (GameControl.Control.Inventory.foodAmount).ToString();
    }


}
