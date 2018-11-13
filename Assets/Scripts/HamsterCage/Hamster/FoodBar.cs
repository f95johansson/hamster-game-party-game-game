using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodBar : MonoBehaviour
{
    public Slider HamsterFoodBar;
    public Transform target;

    private Slider HamsterFood;



    // Use this for initialization
    void Start()
    {
        Debug.Log("HamsterFood", HamsterFood);
        Debug.Log("Canvas", GameObject.FindGameObjectWithTag("Canvas"));

        HamsterFood = Instantiate(HamsterFoodBar);
        HamsterFood.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
    }

    private void Awake()
    {
        target = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        HamsterFood.transform.position = Camera.main.WorldToScreenPoint(target.position);
    }
}
