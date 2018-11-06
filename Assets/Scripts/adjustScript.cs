using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adjustScript : MonoBehaviour {

    private void OnGUI()
    {
        if(GUI.Button(new Rect(10,100,200,30),"health up")) {
            GameControl.control.health += 10;
        }
        if (GUI.Button(new Rect(10, 140, 200, 30), "health down"))
        {
            GameControl.control.health -= 10;
        }
        if (GUI.Button(new Rect(10, 180, 200, 30), "experience up"))
        {
            GameControl.control.experience += 10;
        }
        if (GUI.Button(new Rect(10, 220, 200, 30), "experience down"))
        {
            GameControl.control.experience -= 10;
        }
        if (GUI.Button(new Rect(10, 340, 200, 30), "Add 10000 Money")) {
            GameControl.control.Money += 10000;
            GameControl.control.saveInventory();
        }
        if (GUI.Button(new Rect(10, 260, 200, 30), "save"))
        {
            GameControl.control.save();
        }
        if (GUI.Button(new Rect(10, 300, 200, 30), "load"))
        {
            GameControl.control.load();
        }
    }
}
