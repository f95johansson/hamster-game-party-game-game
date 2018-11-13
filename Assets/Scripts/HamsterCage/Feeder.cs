using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Feeder : MonoBehaviour {

    public Component foodImage;
    public GameObject foodPrefab;
    public Text missingFood;

    private GameObject newFood;
    private Camera camera; 


    // Use this for initialization
    void Start () {
        camera = Camera.main;
        missingFood.text = "";
        Events.OnEvent(UnityEngine.EventSystems.EventTriggerType.PointerDown, foodImage, e =>
        {
            if (GameControl.Control.Inventory.foodAmount > 0)
            {
                newFood = Instantiate(foodPrefab);
            }
            else
            {
                missingFood.text = "You don't have food!";
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
            }
        }
       
		
	}

    
}
