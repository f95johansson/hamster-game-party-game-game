using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public string Axis = "x";
    public float RotateSpeed = 20f;

    private void Update ()
    {
        if (Axis == "x") {
            transform.Rotate(new Vector3(RotateSpeed * Time.deltaTime, 0, 0));
        } else if (Axis == "y") {
            transform.Rotate(new Vector3(0, RotateSpeed * Time.deltaTime, 0));
        } else if (Axis == "z") {
            transform.Rotate(new Vector3(0, 0, RotateSpeed * Time.deltaTime));  
        }
    }
}
