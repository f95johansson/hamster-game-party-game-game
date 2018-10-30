using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adjustScript : MonoBehaviour {

    private void OnGUI()
    {
        if(GUI.Button(new Rect(10,100,100,30),"health up")) {
            GameControl.control.health += 10;
        }
        if (GUI.Button(new Rect(10, 140, 100, 30), "health down"))
        {
            GameControl.control.health -= 10;
        }
        if (GUI.Button(new Rect(10, 180, 100, 30), "experience up"))
        {
            GameControl.control.experience += 10;
        }
        if (GUI.Button(new Rect(10, 220, 100, 30), "experience down"))
        {
            GameControl.control.experience -= 10;
        }
        if (GUI.Button(new Rect(10, 260, 100, 30), "save"))
        {
            GameControl.control.save();
        }
        if (GUI.Button(new Rect(10, 300, 100, 30), "load"))
        {
            GameControl.control.load();
        }
    }
}
