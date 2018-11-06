﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adjustScript : MonoBehaviour {

    private void OnGUI()
    {

        GUI.Label(new Rect(10, 10, 100, 30), "Money: " + GameControl.Control.Inventory.moneyAmount);
        GUI.Label(new Rect(10, 40, 100, 30), "Food: " + GameControl.Control.Inventory.foodAmount);

        if (GUI.Button(new Rect(10, 180, 200, 30), "experience up"))
        {
            GameControl.Control.PlayerData.experience += 10;
        }
        if (GUI.Button(new Rect(10, 220, 200, 30), "experience down"))
        {
            GameControl.Control.PlayerData.experience -= 10;
        }
        if (GUI.Button(new Rect(10, 340, 200, 30), "Add 10000 Money")) {
            //GameControl.Control.inventory.moneyAmount += 10000;
            GameControl.Control.Inventory.moneyAmount += 10000;
            //GameControl.control.saveHamsterCage();
        }
    }
}
