using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HamsterController : MonoBehaviour {

    public Camera camera;
    public GameObject hamster;
    public Text foodAmountText;

    private float maxWidth;


    // Use this for initialization
    void Start()
    {
        if (camera == null)
        {
            camera = Camera.main;
        }
        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0);
        Vector3 targetWidth = camera.ScreenToWorldPoint(upperCorner);
        float hamsterWidth = hamster.GetComponent<Renderer>().bounds.extents.x;
        maxWidth = targetWidth.x - hamsterWidth;

        UpdateFoodText();
        
        SpawnHamsters();
    }

    public void FixedUpdate()
    {
        UpdateFoodText();
    }



    public void SpawnHamsters()
    {
        int i = 0;
        while (GameControl.Control.Inventory.hamsterStates[i]!=null)
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(-maxWidth, maxWidth),
                transform.position.y,
                transform.position.z);
            GameObject hamsterInScene = Instantiate(hamster, spawnPosition, Quaternion.identity);
            hamsterInScene.GetComponent<HamsterState>().weightLevel = GameControl.Control.Inventory.hamsterStates[i].weightLevel;
            //hamsterInScene.GetComponent<HamsterState>().fearLevel = GameControl.Control.Inventory.hamsterStates[i].fearLevel;
            hamsterInScene.GetComponent<HamsterState>().UpdateScaleWeight();
        }
       
        
    }

    public void UpdateFoodText()
    {
        foodAmountText.text = "Food Amount : " + GameControl.Control.Inventory.foodAmount;
    }

}
