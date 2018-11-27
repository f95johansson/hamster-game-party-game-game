using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adjustScript : MonoBehaviour {

    private void OnGUI()
    {

        GUI.Label(new Rect(10, 30, 100, 30), "Money: " + GameControl.Control.Inventory.moneyAmount);

        if (GUI.Button(new Rect(10, 70, 200, 30), "Add 10000 Money")) {
            //GameControl.Control.inventory.moneyAmount += 10000;
            GameControl.Control.Inventory.moneyAmount += 10000;
            //GameControl.control.saveHamsterCage();
        }


        if (GUI.Button(new Rect(10, 110, 200, 30), "Create First Hamster"))
        {
            GameControl.Control.Inventory.hamsterStates[0] = new HamsterState()
            {
                WeightLevel = (uint)UnityEngine.Random.Range(1, 5),
                SpeedLevel = (uint)UnityEngine.Random.Range(1, 5),
                FrictionLevel = (uint)UnityEngine.Random.Range(1, 5),
                TurnSpeedLevel = (uint)UnityEngine.Random.Range(1, 5)
            };
        }
    }
}
